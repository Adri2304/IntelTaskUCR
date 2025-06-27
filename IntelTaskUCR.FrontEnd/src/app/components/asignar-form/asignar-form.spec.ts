import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AsignarForm } from './asignar-form';

describe('AsignarForm', () => {
  let component: AsignarForm;
  let fixture: ComponentFixture<AsignarForm>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AsignarForm]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AsignarForm);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
