import { BrowserModule } from '@angular/platform-browser';
import { NgModule, enableProdMode, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HttpModule } from '@angular/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { PresignComponent } from './presign/presign.component';
import { FinishAuthComponent } from './presign/finish-auth/finish-auth.component';
import { HomeComponent } from './home/home.component';
import { TrackListComponent } from './home/track-list/track-list.component';
import { DeauthorizeComponent } from './deauthorize/deauthorize.component'

import { appRoutes } from './routes';

import { KeyService } from './key.service';
import { SerializeTracksService } from './home/serialize-tracks.service';
import { CookieService } from 'ngx-cookie-service';
import { DeauthorizeUserService } from './user/deauthorize-user.service';
import { SpotifyLogoComponent } from './spotify-logo/spotify-logo.component';
import { RoundTwoDecPipe } from './round-two-dec.pipe';
import { CreatePlaylistComponent } from './home/track-list/create-playlist/create-playlist.component';

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
    CreatePlaylistComponent
  ],
  imports: [
    BrowserModule,
    HttpModule,
    BrowserAnimationsModule,
    RouterModule.forRoot(appRoutes)
  ],
  providers: [
    KeyService,
    SerializeTracksService,
    CookieService,
    DeauthorizeUserService
  ],
  schemas: [
    CUSTOM_ELEMENTS_SCHEMA
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
