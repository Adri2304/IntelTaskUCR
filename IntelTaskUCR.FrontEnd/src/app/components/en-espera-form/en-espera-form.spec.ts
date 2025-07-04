import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EnEsperaForm } from './en-espera-form';

describe('EnEsperaForm', () => {
  let component: EnEsperaForm;
  let fixture: ComponentFixture<EnEsperaForm>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EnEsperaForm]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EnEsperaForm);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
