import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SaidaEstacionamentoComponent } from './saida-estacionamento.component';

describe('SaidaEstacionamentoComponent', () => {
  let component: SaidaEstacionamentoComponent;
  let fixture: ComponentFixture<SaidaEstacionamentoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SaidaEstacionamentoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SaidaEstacionamentoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
