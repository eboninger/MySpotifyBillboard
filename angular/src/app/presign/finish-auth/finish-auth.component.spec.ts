import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FinishAuthComponent } from './finish-auth.component';

describe('FinishAuthComponent', () => {
  let component: FinishAuthComponent;
  let fixture: ComponentFixture<FinishAuthComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FinishAuthComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FinishAuthComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
