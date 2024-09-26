import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { UuidService } from './uuid.service';
import { SignalRService } from '../../core/services/signal-r.service';
import { TokenService } from '../../core/services/token.service';

@Component({
  selector: 'app-encoder',
  templateUrl: './encoder.component.html',
  styleUrls: ['./encoder.component.css']
})
export class EncoderComponent implements OnInit, OnDestroy {
  public encodeForm: FormGroup;
  public encodedText: string = '';
  private operationId: string;
  public isEncoding: boolean = false; 
  constructor(
    private fb: FormBuilder,
    private signalRService: SignalRService,
    private tokenService: TokenService,
    private uuidService: UuidService)
    {
      this.encodeForm = this.fb.group({
        textInput: ['', Validators.required]
      });
    }

  ngOnInit() {
    var token = this.tokenService.getToken();
    this.signalRService.startConnection(token).then(() => {
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

  public onSubmit(): void {
    if (this.encodeForm.valid) {
      this.sendToEncode(this.encodeForm.value.textInput);
    }
  }

  get textInput() {
    return this.encodeForm.get('textInput');
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
