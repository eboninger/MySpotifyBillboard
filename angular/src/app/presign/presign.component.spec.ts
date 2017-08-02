import { async, ComponentFixture, TestBed, ComponentFixtureAutoDetect } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { PresignComponent } from './presign.component';
import { KeyService } from '../key.service'
import { CookieService } from 'ngx-cookie-service'
import { RouterTestingModule } from '@angular/router/testing'


describe('PresignComponent', () => {
  let component: PresignComponent;
  let fixture: ComponentFixture<PresignComponent>;
  let de: DebugElement;
  let el: HTMLElement;
  let keyService: KeyService;
  let cookieService: CookieService;




  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        PresignComponent
      ],
      imports: [
        RouterTestingModule
      ],
      providers: [
        KeyService,
        CookieService,
        { provide: ComponentFixtureAutoDetect, useValue: true }
      ]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PresignComponent);
    component = fixture.componentInstance;

    de = fixture.debugElement.query(By.css('a'));
    el = de.nativeElement;

    keyService = TestBed.get(KeyService);
    spyOn(keyService, 'getKeys').and.returnValue({"key1": "this", "key2": "that"})
    spyOn(keyService, 'getSingleKey').and.returnValue("keyValue");

    cookieService = TestBed.get(CookieService);
    spyOn(cookieService, 'get').and.returnValue("someId");

    // fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });

  it('should redirect to the authorizeUri', () => {
    expect(el.getAttribute('href')).toContain(component.authorizeUri);
  })

  // it('stub object and injected CookieService should not be the same', () => {
  //   let spotifyId = cookieService.get("this")
  //   expect(spotifyId).toBe("someId")
  //   expect(component.isSignedIn).toEqual(true)
  // })

});


