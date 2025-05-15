import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoaderService {
  private isLoading = new BehaviorSubject<boolean>(false);
  private loadingCount = 0;

  get loading$() {
    return this.isLoading.asObservable();
  }

  show() {
    this.loadingCount++;
    console.log('Loader shown, count:', this.loadingCount);
    this.isLoading.next(true);
  }

  hide() {
    this.loadingCount = Math.max(0, this.loadingCount - 1);
    console.log('Loader hidden, count:', this.loadingCount);
    if (this.loadingCount === 0) {
      this.isLoading.next(false);
    }
  }

  reset() {
    console.log('Loader reset');
    this.loadingCount = 0;
    this.isLoading.next(false);
  }
}