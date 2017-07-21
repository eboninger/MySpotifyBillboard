import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PresignComponent } from './presign.component';

describe('PresignComponent', () => {
  let component: PresignComponent;
  let fixture: ComponentFixture<PresignComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PresignComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PresignComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
