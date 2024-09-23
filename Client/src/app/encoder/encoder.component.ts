import { Component, OnDestroy, OnInit } from '@angular/core';
import { UuidService } from '../services/uuid.service';
import { SignalRService } from '../services/signal-r.service';
import { Config } from "../services/config";

@Component({
  selector: 'app-encoder',
  templateUrl: './encoder.component.html',
  styleUrls: ['./encoder.component.css']
})
export class EncoderComponent implements OnInit, OnDestroy {
  public encodedText: string = '';
  private operationId: string;
  public isEncoding: boolean = false; 
  constructor(private signalRService: SignalRService,
    private uuidService: UuidService) { }

  ngOnInit() {
    this.signalRService.startConnection(this.encodinghubUri()).then(() => {
      this.signalRService.addListener('ReceiveChar', (character: string) => {
        this.encodedText += character;
      });
      this.signalRService.addListener('ReceiveCancellationNotice', () => {
        this.encodedText += ' [Encoding Cancelled]';
        this.isEncoding = false; 
      });
      this.signalRService.addListener('ReceiveSuccessNotice', () => {
        this.isEncoding = false; 
      });
    });
  }

  ngOnDestroy() {
    this.signalRService.cancelEncoding(this.operationId);
    this.signalRService.stopConnection();
  }

  public encodinghubUri() {
    return `${Config.api}/signalr/encodinghub`;
  }

  public sendToEncode(input: string): void {
    this.encodedText = ''; // clear previous data
    this.operationId = this.uuidService.generate();
    this.isEncoding = true; 
    this.signalRService.encodeText(input, this.operationId);
  }

  public cancelEncoding(): void {
    this.signalRService.cancelEncoding(this.operationId);
    this.isEncoding = false; 
  }
}
