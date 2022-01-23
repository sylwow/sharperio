import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { map, switchMap } from 'rxjs/operators';
import { ColumnClient, ColumnDto2, CreateColumnCommand, CreateItemCommand, ItemClient, ItemDto2, ItemDto3, IUpdateColumnCommand, IUpdateItemCommand, TableClient, TableDto2, UpdateColumnCommand, UpdateItemCommand } from '../../generated/web-api-client';

@Component({
  selector: 'app-board',
  templateUrl: './board.component.html',
  styleUrls: ['./board.component.scss']
})
export class BoardComponent implements OnInit {
  table$ = this.route.paramMap.pipe(
    map(params => params.get('id') ?? ''),
    switchMap(id => this.tableClient.get(id))
  );

  addColumn: TableDto2 | null = null;
  columnEdition: ColumnDto2 | null = null;

  addItem: ColumnDto2 | null = null;
  itemEdition: ItemDto3 | null = null;

  constructor(
    private route: ActivatedRoute,
    private tableClient: TableClient,
    private columnClient: ColumnClient,
    private itemClient: ItemClient,
  ) { }

  ngOnInit(): void {
  }

  createColumn(table: TableDto2, title: string) {
    this.addColumn = null;
    if (table.columns === undefined) {
      return;
    }
    const temporatyColumn = new ColumnDto2({ title });
    table.columns.push(temporatyColumn);
    this.columnClient.create(new CreateColumnCommand({
      tableId: table.id,
      title: title.trim()
    })).pipe(
      switchMap(id => this.columnClient.get(id))
    ).subscribe(item => {
      const index = table.columns?.indexOf(temporatyColumn) ?? -1;
      if (index > -1) {
        table.columns?.splice(<number>index, 1, item);
      }
    });
  }

  changeColumnTitle(column: ColumnDto2, title: string) {
    this.columnEdition = null;
    const trimmed = title?.trim();
    if (column.title === trimmed) {
      return;
    }
    const oldTitle = column.title;
    column.title = trimmed
    this.editColumn({ id: column.id, title })
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
    if (column.items === undefined) {
      return;
    }
    const temporatyItem = new ItemDto2({ title });
    column.items.push(temporatyItem);
    this.itemClient.create(new CreateItemCommand({
      columnId: column.id,
      title: title.trim(),
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
    if (item.title === trimmed) {
      return;
    }
    const oldTitle = item.title;
    item.title = trimmed
    this.editItem({ id: item.id, title })
      .subscribe({ error: _ => item.title = oldTitle });
  }

  changeItemNote(item: ItemDto3, note: string) {
    const trimmed = note?.trim();
    if (item.note === trimmed) {
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
      this.editColumn({ id: item.id, isArhived: true })
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
      this.columnClient.delete(<number>item.id)
        .subscribe({ error: _ => column.items?.splice(index, 0, column) });
    }
  }

  private editColumn(updateMode: IUpdateColumnCommand): Observable<any> {
    return this.columnClient.update(<number>updateMode.id, new UpdateColumnCommand(updateMode));
  }

  private editItem(updateModel: IUpdateItemCommand): Observable<any> {
    return this.itemClient.update(<number>updateModel.id, new UpdateItemCommand(updateModel));
  }
}
