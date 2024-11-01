import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, catchError, map, throwError } from "rxjs";

@Injectable({
  providedIn: 'root', 
})
export class APIService{
private headers :HttpHeaders | undefined;
constructor(private http:HttpClient) {

}

get(url: string, params?: any): Observable<Response> {
    return this.http.get<Response>(url).pipe(
      map((res: Response) => {
        return res;
      }),
      catchError(this.handleError.bind(this))
    );
  }
post(url:string,data:any):Observable<Response>{
  const options ={};
  this.setHeaders(options);
  return this.http.post<Response>(url,data).pipe(
    map((res:Response)=>{
      return res;
    }),
    catchError(this.handleError.bind(this))
  )
}

private handleError(error: HttpErrorResponse) {
    let errorMessage = 'Unknown error!';
    if (error.error instanceof HttpErrorResponse) {
      // Client-side errors
      errorMessage = `Error: ${error.error.message}`;
    } else {
      // Server-side errors
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    return throwError(() => new Error(errorMessage));
  }

  private setHeaders(options:any,params?:any):void{
    this.headers = new HttpHeaders();
    this.headers.append('Content-Type','application/json');
    this.headers.append('Accept','application/json');
    options['headers'] = this.headers;
  }
}