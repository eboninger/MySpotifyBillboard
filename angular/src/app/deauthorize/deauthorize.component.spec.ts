import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DeauthorizeComponent } from './deauthorize.component';

describe('DeauthorizeComponent', () => {
  let component: DeauthorizeComponent;
  let fixture: ComponentFixture<DeauthorizeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DeauthorizeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DeauthorizeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
