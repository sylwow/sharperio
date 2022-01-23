import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class TokenService {

  private _token: string = '';

  constructor() { }

  get token() { return this._token; }
  set token(newToken: string) { this._token = newToken; }
}
