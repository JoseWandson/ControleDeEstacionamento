import { Component, OnInit, Inject } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'app-saida-estacionamento',
    templateUrl: './saida-estacionamento.component.html'
})
export class SaidaEstacionamentoComponent implements OnInit {

    saida: any;
    id: number;

    constructor(private http: HttpClient,
        @Inject('BASE_URL') private baseUrl: string,
        private route: ActivatedRoute) {
        this.route.params.subscribe(params => this.id = +params['id']);
    }

    ngOnInit(): void {
        this.saida = { id: this.id };
    }

    marcarSaida(frm: FormGroup): void {
        this.http.post(this.baseUrl + 'estacionamento/saida', this.saida).subscribe(result => {
            frm.reset();
            this.saida = {};
        }, error => console.error(error));
    }

}
