import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthorizeService } from '../services/authorize.service';
import { filter, map } from 'rxjs/operators';
import { Paths } from '../constants/paths';

@Injectable({
  providedIn: 'root'
})
export class AuthorizeGuard implements CanActivate {

  constructor(private authorize: AuthorizeService, private router: Router) {
  }

  canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean> | boolean {
    return this.authorize.isAuthenticated$
      .pipe(
        filter(result => result != undefined),
        map(result => <boolean>result),
        map(isAuthenticated => this.handleAuthorization(isAuthenticated, state))
      );
  }

  private handleAuthorization(isAuthenticated: boolean, state: RouterStateSnapshot): boolean | UrlTree {
    if (isAuthenticated) {
      return true;
    }
    this.router.navigate(['/', Paths.home]);
    return false;
  }
}
