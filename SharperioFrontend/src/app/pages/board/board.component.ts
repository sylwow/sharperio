import { CdkDragDrop, moveItemInArray, transferArrayItem } from '@angular/cdk/drag-drop';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { filter, map, shareReplay, switchMap } from 'rxjs/operators';
import { NotificationService } from 'src/app/services/notification.service';
import { ColumnClient, ColumnDto2, CreateColumnCommand, CreateItemCommand, ItemClient, ItemDto2, ItemDto3, IUpdateColumnCommand, IUpdateItemCommand, TableClient, TableDto2, UpdateColumnCommand, UpdateColumnOrderCommand, UpdateItemCommand, UpdateItemOrderCommand } from '../../generated/web-api-client';

interface ColumOrderChanged {
  tableId: string,
  columnId: number,
  newIndex: number
}

interface ItemOrderChanged {
  tableId: string,
  itemId: number,
  index: number,
  newColumnId: number,
  previousColumnId: number,
}

@Component({
  selector: 'app-board',
  templateUrl: './board.component.html',
  styleUrls: ['./board.component.scss']
})
export class BoardComponent implements OnInit {
  table!: TableDto2

  columnIds!: string[];

  addColumn: TableDto2 | null = null;
  columnEdition: ColumnDto2 | null = null;

  addItem: ColumnDto2 | null = null;
  itemEdition: ItemDto3 | null = null;

  constructor(
    private route: ActivatedRoute,
    private tableClient: TableClient,
    private columnClient: ColumnClient,
    private itemClient: ItemClient,
    private notificationService: NotificationService,
  ) { }

  ngOnInit(): void {
    this.route.paramMap.pipe(
      map(params => params.get('id') ?? ''),
      switchMap(id => this.tableClient.get(id)),
    ).subscribe(table => this.setUpData(table));
  }

  setUpData(table: TableDto2): void {
    this.table = table
    this.columnIds = table?.columns?.map(c => c.id?.toString() ?? '') ?? []
    this.notificationService.connected$.pipe(
      filter(connected => !!connected)
    ).subscribe(_ => {
      this.notificationService.joinGroup(<string>table.id).subscribe();

      this.notificationService.on<ColumOrderChanged>('columnOrderChangedEvent')
        .subscribe(data => {
          if (!table.columns) {
            return;
          }
          const previousIndex = table.columns.findIndex(col => col.id == data.columnId);
          this.moveColumn(table.columns, previousIndex, data.newIndex, true);
        })

      this.notificationService.on<ItemOrderChanged>('itemOrderChangedEvent')
        .subscribe(data => {
          if (!table.columns) {
            return;
          }
          const column = table.columns.find(col => col.id == data.previousColumnId);
          if (!column) {
            return;
          }
          const previousIndex = column?.items?.findIndex(it => it.id == data.itemId);
          if (previousIndex == undefined || previousIndex == -1) {
            return;
          }
          if (data.previousColumnId === data.newColumnId) {
            return this.moveItem(column, previousIndex, data.index, true);
          }
          const newColumn = table.columns.find(col => col.id == data.newColumnId);
          if (!newColumn) {
            return;
          }
          this.transferItem(column, newColumn, previousIndex, data.index, true);
        })
    });
  }

  createColumn(table: TableDto2, title: string) {
    this.addColumn = null;
    const trimmed = title.trim();
    if (table.columns === undefined || !trimmed) {
      return;
    }
    const temporatyColumn = new ColumnDto2({ title: trimmed });
    table.columns.push(temporatyColumn);
    this.columnClient.create(new CreateColumnCommand({
      tableId: table.id,
      title: trimmed
    })).pipe(
      switchMap(id => this.columnClient.get(id))
    ).subscribe(item => {
      const index = table.columns?.indexOf(temporatyColumn) ?? -1;
      if (index > -1) {
        table.columns?.splice(<number>index, 1, item);
        this.columnIds.push(item.id?.toString() ?? '');
      }
    });
  }

  changeColumnTitle(column: ColumnDto2, title: string) {
    this.columnEdition = null;
    const trimmed = title?.trim();
    if (column.title === trimmed || !trimmed) {
      return;
    }
    const oldTitle = column.title;
    column.title = trimmed
    this.editColumn({ id: column.id, title: trimmed })
      .subscribe({ error: _ => column.title = oldTitle });
  }

  changeColumnOrder(column: ColumnDto2, order: number) {
    if (column.order === order) {
      return;
    }
    const oldOrder = column.order;
    column.order = order
    this.editColumn({ id: column.id, order })
      .subscribe({ error: _ => column.order = oldOrder });
  }

  arhieveColumn(table: TableDto2, column: ColumnDto2) {
    if (column.isArhived) {
      return;
    }

    if (table.columns == undefined) {
      return;
    }
    const index = table.columns.indexOf(column);
    if (index > -1) {
      table.columns.splice(index, 1);
      this.editColumn({ id: column.id, isArhived: true })
        .subscribe({ error: _ => table.columns?.splice(index, 0, column) });
    }

  }

  deleteColumn(table: TableDto2, column: ColumnDto2) {
    if (table.columns == undefined) {
      return;
    }
    const index = table.columns.indexOf(column);
    if (index > -1) {
      table.columns.splice(index, 1);
      this.columnClient.delete(<number>column.id)
        .subscribe({ error: _ => table.columns?.splice(index, 0, column) });
    }
  }

  createItem(column: ColumnDto2, title: string, note: string | null = null) {
    this.addItem = null;
    const trimmed = title?.trim();
    if (column.items === undefined || !trimmed) {
      return;
    }
    const temporatyItem = new ItemDto2({ title: trimmed });
    column.items.push(temporatyItem);
    this.itemClient.create(new CreateItemCommand({
      columnId: column.id,
      title: trimmed,
      note: note?.trim() ?? ''
    })).pipe(
      switchMap(id => this.itemClient.get(id))
    ).subscribe(item => {
      const index = column.items?.indexOf(temporatyItem) ?? -1;
      if (index > -1) {
        column.items?.splice(<number>index, 1, item);
      }
    });
  }

  changeItemTitle(item: ItemDto3, title: string) {
    this.itemEdition = null;
    const trimmed = title?.trim();
    if (item.title === trimmed || !trimmed) {
      return;
    }
    const oldTitle = item.title;
    item.title = trimmed
    this.editItem({ id: item.id, title: trimmed })
      .subscribe({ error: _ => item.title = oldTitle });
  }

  changeItemNote(item: ItemDto3, note: string) {
    const trimmed = note?.trim();
    if (item.note === trimmed || !trimmed) {
      return;
    }
    const oldNote = item.note;
    item.note = trimmed
    this.editItem({ id: item.id, note })
      .subscribe({ error: _ => item.note = oldNote });
  }

  changeItemOrder(item: ItemDto3, order: number) {
    if (item.order === order) {
      return;
    }
    const oldOrder = item.order;
    item.order = order
    this.editItem({ id: item.id, order })
      .subscribe({ error: _ => item.order = oldOrder });
  }

  arhieveItem(column: ColumnDto2, item: ItemDto3) {
    if (item.isArhived) {
      return;
    }

    if (column.items == undefined) {
      return;
    }
    const index = column.items.indexOf(item);
    if (index > -1) {
      column.items.splice(index, 1);
      this.editItem({ id: item.id, isArhived: true })
        .subscribe({ error: _ => column.items?.splice(index, 0, column) });
    }
  }

  deleteItem(column: ColumnDto2, item: ItemDto3) {
    if (column.items == undefined) {
      return;
    }
    const index = column.items.indexOf(column);
    if (index > -1) {
      column.items.splice(index, 1);
      this.itemClient.delete(<number>item.id)
        .subscribe({ error: _ => column.items?.splice(index, 0, column) });
    }
  }

  private editColumn(updateMode: IUpdateColumnCommand): Observable<any> {
    return this.columnClient.update(<number>updateMode.id, new UpdateColumnCommand(updateMode));
  }

  private editItem(updateModel: IUpdateItemCommand): Observable<any> {
    return this.itemClient.update(<number>updateModel.id, new UpdateItemCommand(updateModel));
  }

  delayedEditItemTitle(item: ItemDto2) {
    setTimeout(() => this.itemEdition = item, 300);
  }

  drop(event: CdkDragDrop<ItemDto2[]> | any) {
    if (event.previousContainer === event.container) {
      this.moveItem(event.container.data, event.previousIndex, event.currentIndex);
    } else {
      this.transferItem(event.previousContainer.data, event.container.data, event.previousIndex, event.currentIndex);
    }
  }

  dropColumn(event: CdkDragDrop<ItemDto2[]> | any) {
    this.moveColumn(event.container.data, event.previousIndex, event.currentIndex);
  }

  private moveColumn(data: ColumnDto2[], previousIndex: number, currentIndex: number, onlyInUI = false) {
    if (previousIndex === currentIndex) {
      return;
    }
    moveItemInArray(data, previousIndex, currentIndex);
    const column = data[currentIndex];

    onlyInUI || this.columnClient.updateOrder(<number>column.id, new UpdateColumnOrderCommand({
      id: <number>column.id,
      index: currentIndex
    })).subscribe({ error: _ => moveItemInArray(data, currentIndex, previousIndex) });
  }

  private moveItem(data: ColumnDto2, previousIndex: number, currentIndex: number, onlyInUI = false) {
    if (!data.items || previousIndex === currentIndex) {
      return;
    }
    moveItemInArray(data.items, previousIndex, currentIndex);
    const item = data.items[currentIndex];

    onlyInUI || this.itemClient.updateOrder(<number>item.id, new UpdateItemOrderCommand({
      id: <number>item.id,
      index: currentIndex,
      newColumnId: data.id,
      previousColumnId: data.id
    })).subscribe({ error: _ => moveItemInArray(data.items ?? [], currentIndex, previousIndex) });
  }

  private transferItem(previous: ColumnDto2, current: ColumnDto2, previousIndex: number, currentIndex: number, onlyInUI = false) {
    if (!previous.items || !current.items) {
      return;
    }
    transferArrayItem(previous.items, current.items, previousIndex, currentIndex);
    const item = current.items[currentIndex];

    onlyInUI || this.itemClient.updateOrder(<number>item.id, new UpdateItemOrderCommand({
      id: <number>item.id,
      index: currentIndex,
      newColumnId: current.id,
      previousColumnId: previous.id
    })).subscribe({ error: _ => transferArrayItem(current.items ?? [], previous.items ?? [], currentIndex, previousIndex) });
  }
}
