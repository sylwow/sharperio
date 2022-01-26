import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { HttpTransportType } from '@microsoft/signalr';
import { BehaviorSubject, from, Observable, Subject } from 'rxjs';
import { switchMap, tap } from 'rxjs/operators';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  private hubConnection!: signalR.HubConnection;
  private signalRUrl = `${environment.apiRoot}/notifications`;

  private currentGroupName: string = '';
  private connected = new BehaviorSubject<boolean>(false);

  connected$ = this.connected.asObservable();

  constructor() {
  }

  initialize() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(this.signalRUrl)
      .build();

    from(this.hubConnection.start()).subscribe({
      next: _ => this.onConnected(),
      error: error => this.onConnectedFail(error)
    });
  }

  joinGroup(groupName: string): Observable<void> {
    if (!this.currentGroupName) {
      return from(this.hubConnection.send('joinNotification', groupName));
    }

    return from(this.hubConnection.send('leaveNotification', this.currentGroupName)).pipe(
      switchMap(_ => from(this.hubConnection.send('joinNotification', groupName))),
      tap(_ => this.currentGroupName = groupName)
    );
  }

  on<T>(method: string): Observable<T> {
    const notifier = new Subject<T>();
    this.hubConnection.on(method, data =>
      notifier.next(data)
    );
    return notifier.asObservable();
  }

  private onConnected(): void {
    console.log('Connected to notification service');
    this.connected.next(true);
  }

  private onConnectedFail(error: any): void {
    console.log('Error while starting connection: ' + error)
    this.connected.next(false);
  }
}
