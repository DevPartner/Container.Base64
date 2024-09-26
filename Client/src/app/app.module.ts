import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { ModalModule } from 'ngx-bootstrap/modal';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './features/nav-menu/nav-menu.component';
import { EncoderComponent } from './features/encoder/encoder.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    EncoderComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    RouterModule.forRoot([
      { path: 'encoder', component: EncoderComponent },
      { path: '', redirectTo: 'encoder', pathMatch: 'full' },
      { path: '**', redirectTo: 'encoder' } // Wildcard route
    ]),
    BrowserAnimationsModule,
    ModalModule.forRoot()
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
