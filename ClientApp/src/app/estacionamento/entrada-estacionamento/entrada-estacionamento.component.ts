import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormGroup } from '@angular/forms';

@Component({
    selector: 'app-entrada-estacionamento',
    templateUrl: './entrada-estacionamento.component.html'
})
export class EntradaEstacionamentoComponent implements OnInit {

    entrada: any;

    constructor(private http: HttpClient,
        @Inject('BASE_URL') private baseUrl: string) { }

    ngOnInit(): void {
        this.entrada = {};
    }

    marcarEntrada(frm: FormGroup): void {
        this.http.post(this.baseUrl + 'estacionamento/entrada', this.entrada).subscribe(result => {
            frm.reset();
            this.entrada = {};
        }, error => console.error(error));;
    }

}
