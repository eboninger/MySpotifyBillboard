import { BrowserModule } from '@angular/platform-browser';
import { NgModule, enableProdMode, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HttpModule } from '@angular/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BusyModule } from 'angular2-busy'
import { NgbModule } from '@ng-bootstrap/ng-bootstrap'

import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { PresignComponent } from './presign/presign.component';
import { FinishAuthComponent } from './presign/finish-auth/finish-auth.component';
import { HomeComponent } from './home/home.component';
import { TrackListComponent } from './home/track-list/track-list.component';
import { DeauthorizeComponent } from './deauthorize/deauthorize.component'
import { CreatePlaylistComponent } from './home/track-list/create-playlist/create-playlist.component';
import { RecordsComponent } from './home/records/records.component';
import { SingleRecordComponent } from './home/records/single-record/single-record.component';
import { SpotifyLogoComponent } from './spotify-logo/spotify-logo.component';

import { appRoutes } from './routes';

import { KeyService } from './key.service';
import { SerializeTracksService } from './home/serialize-tracks.service';
import { CookieService } from 'ngx-cookie-service';
import { DeauthorizeUserService } from './user/deauthorize-user.service';
import { TopTracksService } from './home/track-list/top-tracks.service';
import { CreatePlaylistService } from './home/track-list/create-playlist/create-playlist.service'

import { RoundTwoDecPipe } from './round-two-dec.pipe';


// uncomment line below to enable production mode
// enableProdMode();


@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    PresignComponent,
    FinishAuthComponent,
    HomeComponent,
    TrackListComponent,
    DeauthorizeComponent,
    SpotifyLogoComponent,
    RoundTwoDecPipe,
    CreatePlaylistComponent,
    RecordsComponent,
    SingleRecordComponent
  ],
  imports: [
    BrowserModule,
    HttpModule,
    BrowserAnimationsModule,
    BusyModule,
    NgbModule.forRoot(),
    RouterModule.forRoot(appRoutes)
  ],
  providers: [
    KeyService,
    SerializeTracksService,
    CookieService,
    DeauthorizeUserService,
    TopTracksService,
    CreatePlaylistService
  ],
  schemas: [
    CUSTOM_ELEMENTS_SCHEMA
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
