import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  private hubConnection: HubConnection;

  public startConnection(url: string): Promise<void> {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(url, { withCredentials: true })
      .build();

    return this.hubConnection.start();
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
