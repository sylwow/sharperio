import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Paths } from '../constants/paths';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.scss']
})
export class NavMenuComponent implements OnInit {
  paths = Paths;

  isExpanded = false;

  isProduction: boolean = false;

  ngOnInit(): void {
    this.isProduction = environment.production;
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
