import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { OverviewComponent } from './src/admin/overview/overview.component';
import { ReadConfigService } from './src/admin/services/read-config.service';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    AppComponent,
    OverviewComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    ReactiveFormsModule
  ],
  providers: [
    ReadConfigService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
