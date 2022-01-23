import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Paths } from '../constants/paths';
import { TableClient, TableDto } from '../generated/web-api-client';

@Component({
  selector: 'app-welcome',
  templateUrl: './welcome.component.html',
  styleUrls: ['./welcome.component.scss']
})
export class WelcomeComponent implements OnInit {
  paths = Paths

  allTables: TableDto[] | undefined;

  constructor(
    private tableClient: TableClient,
    private router: Router) { }

  ngOnInit(): void {
    this.tableClient.getList().subscribe({
      next: tables => this.allTables = tables,
    });
  }
}
