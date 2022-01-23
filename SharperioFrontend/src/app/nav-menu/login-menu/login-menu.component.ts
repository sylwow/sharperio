import { Component, OnInit } from '@angular/core';
import { AuthorizeService } from '../../services/authorize.service';
import { Observable, of } from 'rxjs';

@Component({
  selector: 'app-login-menu',
  templateUrl: './login-menu.component.html',
  styleUrls: ['./login-menu.component.scss']
})
export class LoginMenuComponent implements OnInit {

  public isAuthenticated$ = this.authorizeService.isAuthenticated$;
  public userName: Observable<string> = of('bob');

  constructor(public authorizeService: AuthorizeService) { }

  ngOnInit() {
  }

  login() {
    this.authorizeService.login();
  }

  logout() {
    this.authorizeService.logout();
  }
}
