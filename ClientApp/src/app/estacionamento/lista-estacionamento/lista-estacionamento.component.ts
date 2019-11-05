import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
    selector: 'app-lista-estacionamento',
    templateUrl: './lista-estacionamento.component.html'
})
export class ListaEstacionamentoComponent {

    public estacionamento: Estacionamento[];

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        http.get<Estacionamento[]>(baseUrl + 'estacionamento').subscribe(result => {
            this.estacionamento = result;
        }, error => console.error(error));
    }

}

interface Estacionamento {
    id: number;
    placa: string;
    horarioChegada: string;
    horarioSaida: string;
    duracao: string;
    tempoCobrado: number;
    preco: number;
    valorAPagar: number;
}
