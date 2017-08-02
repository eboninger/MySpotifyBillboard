import { TestBed, async } from '@angular/core/testing';

import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component'
import { SpotifyLogoComponent } from './spotify-logo/spotify-logo.component' 
import { RouterTestingModule } from '@angular/router/testing'

describe('AppComponent', () => {
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        AppComponent,
        NavComponent,
        SpotifyLogoComponent
      ],
      imports: [RouterTestingModule]
    }).compileComponents();
  }));

});
