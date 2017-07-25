import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HttpModule } from '@angular/http';

import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { PresignComponent } from './presign/presign.component';

import { appRoutes } from './routes';
import { KeyService } from './key.service';
import { UserDataService } from './user-data.service'
import { FinishAuthComponent } from './presign/finish-auth/finish-auth.component';
import { HomeComponent } from './home/home.component';


@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    PresignComponent,
    FinishAuthComponent,
    HomeComponent
  ],
  imports: [
    BrowserModule,
    HttpModule,
    RouterModule.forRoot(appRoutes)
  ],
  providers: [
    KeyService,
    UserDataService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
