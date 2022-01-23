import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Paths } from 'src/app/constants/paths';
import { TableClient, TableDto } from 'src/app/generated/web-api-client';

@Component({
  selector: 'app-welcome',
  templateUrl: './welcome.component.html',
  styleUrls: ['./welcome.component.scss']
})
export class WelcomeComponent implements OnInit {
  paths = Paths

  allTables$ = this.tableClient.getList()
    .pipe();

  constructor(
    private tableClient: TableClient,
    private router: Router) { }

  ngOnInit(): void {
  }
}
