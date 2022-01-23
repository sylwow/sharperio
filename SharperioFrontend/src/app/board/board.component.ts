import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { map, switchMap } from 'rxjs/operators';
import { ColumnClient, ColumnDto2, CreateColumnCommand, CreateItemCommand, ItemClient, ItemDto3, TableClient, TableDto2, UpdateColumnCommand, UpdateItemCommand } from '../generated/web-api-client';

@Component({
  selector: 'app-board',
  templateUrl: './board.component.html',
  styleUrls: ['./board.component.scss']
})
export class BoardComponent implements OnInit {
  table!: TableDto2;

  columnEdition: ColumnDto2 | null = null;
  addItem: ColumnDto2 | null = null;
  itemEdition: ItemDto3 | null = null;
  addColumn: TableDto2 | null = null;

  constructor(
    private route: ActivatedRoute,
    private tableClient: TableClient,
    private columnClient: ColumnClient,
    private itemClient: ItemClient,
  ) { }

  ngOnInit(): void {
    this.route.paramMap.pipe(
      map(params => params.get('id') ?? ''),
      switchMap(id => this.tableClient.get(id))
    ).subscribe(table => this.table = table);
  }

  createColumn(table: TableDto2, title: string) {
    this.addColumn = null;
    this.columnClient.create(new CreateColumnCommand({
      tableId: table.id,
      title: title.trim()
    })).pipe(
      switchMap(id => this.columnClient.get(id))
    ).subscribe(item => table.columns?.push(item));
  }

  editColumn(column: ColumnDto2, title?: string, isArhived?: boolean, order?: number) {
    this.columnEdition = null;
    const trimmed = title?.trim();

    if (column.title === trimmed &&
      column.isArhived === isArhived &&
      column.order === order) {
      return;
    }

    this.columnClient.update(<number>column.id, new UpdateColumnCommand({
      id: <number>column.id,
      title: trimmed,
      isArhived,
      order
    })).subscribe(_ => {
      column.title = trimmed ?? column.title
      column.isArhived = isArhived ?? column.isArhived
      column.order = order ?? column.order
    });
  }

  deleteColumn(table: TableDto2, column: ColumnDto2) {
    this.columnClient.delete(<number>column.id)
      .subscribe(_ => table.columns = table.columns?.filter(col => col != column));
  }

  createItem(column: ColumnDto2, title: string, note: string | null = null) {
    this.addItem = null;

    this.itemClient.create(new CreateItemCommand({
      columnId: column.id,
      title: title.trim(),
      note: note?.trim() ?? ''
    })).pipe(
      switchMap(id => this.itemClient.get(id))
    ).subscribe(item => column.items?.push(item));
  }

  editItem(item: ItemDto3, title?: string, note?: string, order?: number, isArhived?: boolean) {
    this.columnEdition = null;
    const trimmed = title?.trim();

    if (item.title === trimmed &&
      item.isArhived === isArhived &&
      item.note === note &&
      item.order === order) {
      return;
    }
    this.itemClient.update(<number>item.id, new UpdateItemCommand({
      id: <number>item.id,
      title: trimmed,
      note,
      isArhived,
      order
    })).subscribe(_ => {
      item.title = trimmed ?? item.title
      item.isArhived = isArhived ?? item.isArhived
      item.note = note ?? item.note
      item.order = order ?? item.order
    });
  }

  deleteItem(column: ColumnDto2, item: ItemDto3) {
    this.itemClient.delete(<number>item.id)
      .subscribe(_ => column.items = column.items?.filter(it => it != item));
  }
}
