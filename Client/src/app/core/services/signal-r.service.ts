import { Injectable } from '@angular/core';
//import { HubConnection, HubConnectionBuilder, DefaultHttpClient, LogLevel } from '@microsoft/signalr';
import * as signalR from '@microsoft/signalr';

import { Config } from "./config";
import { CustomHttpClient } from "./custom-httpclient";

@Injectable({
  providedIn: 'root'
})

export class SignalRService {
  private hubConnection: signalR.HubConnection;
  constructor() { }

  public startConnection(token: string): Promise<void> {
    var url = this.encodinghubUri();


    fetch(`${Config.api}/api/health`, {
      method: 'GET', 
      headers: {
        'Authorization': token
      }
    }).then(response => {
      if (response.ok) {
        console.log('Authentication successful!');
        // Handle the authenticated response, load data, etc.
      } else if (response.status === 401) {
        console.error('Authentication failed. Incorrect login.');
      }
    }).catch(error => {
      console.error('Error:', error);
    });

    const defaultHttpClient = new signalR.DefaultHttpClient(signalR.NullLogger.instance);
    const customHttpClient = new CustomHttpClient(defaultHttpClient, token);

    this.hubConnection = new signalR.HubConnectionBuilder()
      .configureLogging(signalR.LogLevel.Trace)
      .withUrl(url, {
        withCredentials: true,
        httpClient: customHttpClient,
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
