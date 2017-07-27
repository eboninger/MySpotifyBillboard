import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SpotifyLogoComponent } from './spotify-logo.component';

describe('SpotifyLogoComponent', () => {
  let component: SpotifyLogoComponent;
  let fixture: ComponentFixture<SpotifyLogoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SpotifyLogoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SpotifyLogoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
