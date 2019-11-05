import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { ListaEstacionamentoComponent } from './estacionamento/lista-estacionamento/lista-estacionamento.component';
import { EntradaEstacionamentoComponent } from './estacionamento/entrada-estacionamento/entrada-estacionamento.component';
import { SaidaEstacionamentoComponent } from './estacionamento/saida-estacionamento/saida-estacionamento.component';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        ListaEstacionamentoComponent,
        EntradaEstacionamentoComponent,
        SaidaEstacionamentoComponent
    ],
    imports: [
        BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
        HttpClientModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', component: HomeComponent, pathMatch: 'full' },
            { path: 'estacionamento', component: ListaEstacionamentoComponent },
            { path: 'entrada', component: EntradaEstacionamentoComponent },
            { path: 'saida/:id', component: SaidaEstacionamentoComponent }
        ])
    ],
    providers: [],
    bootstrap: [AppComponent]
})
export class AppModule { }
