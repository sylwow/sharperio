import { Injectable } from '@angular/core';
import { BehaviorSubject, combineLatest, Observable } from 'rxjs';
import { map, } from 'rxjs/operators';
import { AuthConfig, OAuthService } from 'angular-oauth2-oidc';
import { setToken } from './token';

export const authConfig: AuthConfig = {

  // Url of the Identity Provider
  issuer: 'http://localhost:8080/auth/realms/sharperio',

  // URL of the SPA to redirect the user to after login
  redirectUri: window.location.origin,

  // The SPA's id. 
  // The SPA is registerd with this id at the auth-server
  clientId: 'sharperio',

  // set the scope for the permissions the client should request
  // The first three are defined by OIDC.
  scope: 'openid profile email',
  // Remove the requirement of using Https to simplify the demo
  // THIS SHOULD NOT BE USED IN PRODUCTION
  // USE A CERTIFICATE FOR YOUR IDP
  // IN PRODUCTION
  requireHttps: false
}

@Injectable({
  providedIn: 'root'
})
export class AuthorizeService {

  private isAuthenticatedSubject$ = new BehaviorSubject<boolean>(false);
  isAuthenticated$ = this.isAuthenticatedSubject$.asObservable();

  private isDoneLoadingSubject$ = new BehaviorSubject<boolean>(false);
  isDoneLoading$ = this.isDoneLoadingSubject$.asObservable();


  /**
   * Publishes `true` if and only if (a) all the asynchronous initial
   * login calls have completed or errorred, and (b) the user ended up
   * being authenticated.
   *
   * In essence, it combines:
   *
   * - the latest known state of whether the user is authorized
   * - whether the ajax calls for initial log in have all been done
   */
  canActivateProtectedRoutes$: Observable<boolean> = combineLatest([
    this.isAuthenticated$,
    this.isDoneLoading$
  ]).pipe(map(values => values.every(b => b)));

  constructor(
    private oauthService: OAuthService,
  ) {
    this.oauthService.configure(authConfig);
    // this.oauthService.tokenValidationHandler = new JwksValidationHandler();
    this.oauthService.loadDiscoveryDocumentAndTryLogin();

    window.addEventListener('storage', (event) => {
      // The `key` is `null` if the event was caused by `.clear()`
      if (event.key !== 'access_token' && event.key !== null) {
        return;
      }

      console.warn('Noticed changes to access_token (most likely from another tab), updating isAuthenticated');
      this.isAuthenticatedSubject$.next(this.oauthService.hasValidAccessToken());
    });

    this.oauthService.events
      .subscribe(_ => {
        this.isAuthenticatedSubject$.next(this.oauthService.hasValidAccessToken());
      });

    this.isAuthenticated$.subscribe(_ => {
      setToken(this.accessToken);
    })
  }

  login() {
    this.oauthService.initLoginFlow();
  }

  logout() { this.oauthService.logOut(); }
  refresh() { this.oauthService.silentRefresh(); }
  hasValidToken() { return this.oauthService.hasValidAccessToken(); }

  get givenName() {
    let claims = this.oauthService.getIdentityClaims();
    if (!claims) return null;
    return claims['given_name'];
  }

  // These normally won't be exposed from a service like this, but
  // for debugging it makes sense.
  get accessToken() { return this.oauthService.getAccessToken(); }
  get refreshToken() { return this.oauthService.getRefreshToken(); }
  get identityClaims() { return this.oauthService.getIdentityClaims(); }
  get idToken() { return this.oauthService.getIdToken(); }
  get logoutUrl() { return this.oauthService.logoutUrl; }
}
