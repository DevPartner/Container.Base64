import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';

import { Config } from "../../core/services/config";

@Injectable({
  providedIn: 'root'
})

export class SignalRService {
  private hubConnection: signalR.HubConnection;
  constructor() { }

  public startConnection(): Promise<void> {
    var url = this.encodinghubUri();

    this.hubConnection = new signalR.HubConnectionBuilder()
      //.configureLogging(signalR.LogLevel.Trace)
      .withUrl(url, {
        withCredentials: true,
        transport: signalR.HttpTransportType.LongPolling
      })
      .build();

    return this.hubConnection.start();
  }

  public encodinghubUri() {
    return `${Config.api}/signalr/encodinghub`;
  }

  public stopConnection(): Promise<void> {
    return this.hubConnection.stop();
  }
  public addListener(eventName: string, action: (...args: any[]) => void): void {
    this.hubConnection.on(eventName, action);
  }

  public encodeText(input: string, operationId: string): Promise<void> {
    return this.hubConnection.invoke('EncodeText', input, operationId);
  }

  public cancelEncoding(operationId: string): Promise<void> {
    return this.hubConnection.invoke('CancelEncoding', operationId);
  }
}
