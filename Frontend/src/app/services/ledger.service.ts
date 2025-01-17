import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Ledger } from '../models/ledger.interface';
import { AuthService } from './auth.service';

@Injectable({
    providedIn: 'root',
})
export class LedgerService {
    private apiUrl = 'http://localhost:5000/api/v1';

    constructor(
        private http: HttpClient,
        private authService: AuthService,
    ) {}

    getLedgers(): Observable<Ledger[]> {
        const token = this.authService.getToken();
        if (token) {
            return this.http.get<Ledger[]>(`${this.apiUrl}/ledgers`);
        }

        return new Observable<Ledger[]>();
    }

    deleteLedger(ledgerId: number){
        return this.http.delete(`${this.apiUrl}/ledgers/${ledgerId}`)
    }

    create(id: number, name: string, balance: number) {
        const Ledger = {
            id,
            name,
            balance,
        };
            return this.http.post(`${this.apiUrl}/ledgers`, Ledger)
    }

    deleteLedger(ledgerId: number){
        return this.http.delete(`${this.apiUrl}/ledgers/${ledgerId}`)
    }

<<<<<<< HEAD
    create(id: number, name: string, balance: number) {
        const Ledger = {
            id,
            name,
            balance,
        };
            return this.http.post(`${this.apiUrl}/ledgers`, Ledger)
    }

=======
>>>>>>> ce83984 (add delete method in frontend)
    transferFunds(fromLedgerId: number, toLedgerId: number, amount: number): Observable<unknown> {
        const payload = {
            fromLedgerId,
            toLedgerId,
            amount,
        };
        return this.http.post(`${this.apiUrl}/ledgers/transfer`, payload);
    }
}
