//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v14.4.0.0 (NJsonSchema v11.3.2.0 (Newtonsoft.Json v13.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------

/* tslint:disable */
/* eslint-disable */
// ReSharper disable InconsistentNaming

import { mergeMap as _observableMergeMap, catchError as _observableCatch } from 'rxjs/operators';
import { Observable, throwError as _observableThrow, of as _observableOf } from 'rxjs';
import { Injectable, Inject, Optional, InjectionToken } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse, HttpResponseBase } from '@angular/common/http';

export const API_BASE_URL = new InjectionToken<string>('API_BASE_URL');

@Injectable({
    providedIn: 'root'
})
export class IncomeTaxClient {
    private http: HttpClient;
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(@Inject(HttpClient) http: HttpClient, @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
        this.http = http;
        this.baseUrl = baseUrl ?? "";
    }

    /**
     * @param body (optional) 
     * @return Success
     */
    calculate(body: CalculateIncomeTaxCommand | undefined): Observable<TaxBreakdownDto> {
        let url_ = this.baseUrl + "/api/tax/calculate";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(body);

        let options_ : any = {
            body: content_,
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Content-Type": "application/json",
                "Accept": "text/plain"
            })
        };

        return this.http.request("post", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processCalculate(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processCalculate(response_ as any);
                } catch (e) {
                    return _observableThrow(e) as any as Observable<TaxBreakdownDto>;
                }
            } else
                return _observableThrow(response_) as any as Observable<TaxBreakdownDto>;
        }));
    }

    protected processCalculate(response: HttpResponseBase): Observable<TaxBreakdownDto> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (response as any).error instanceof Blob ? (response as any).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result200 = TaxBreakdownDto.fromJS(resultData200);
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<TaxBreakdownDto>(null as any);
    }

    /**
     * @return Success
     */
    tax(): Observable<SalaryDto> {
        let url_ = this.baseUrl + "/api/tax";
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Accept": "text/plain"
            })
        };

        return this.http.request("get", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processTax(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processTax(response_ as any);
                } catch (e) {
                    return _observableThrow(e) as any as Observable<SalaryDto>;
                }
            } else
                return _observableThrow(response_) as any as Observable<SalaryDto>;
        }));
    }

    protected processTax(response: HttpResponseBase): Observable<SalaryDto> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (response as any).error instanceof Blob ? (response as any).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result200 = SalaryDto.fromJS(resultData200);
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<SalaryDto>(null as any);
    }
}

export class CalculateIncomeTaxCommand implements ICalculateIncomeTaxCommand {
    annualSalaryAmount?: number;

    constructor(data?: ICalculateIncomeTaxCommand) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.annualSalaryAmount = _data["annualSalaryAmount"];
        }
    }

    static fromJS(data: any): CalculateIncomeTaxCommand {
        data = typeof data === 'object' ? data : {};
        let result = new CalculateIncomeTaxCommand();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["annualSalaryAmount"] = this.annualSalaryAmount;
        return data;
    }
}

export interface ICalculateIncomeTaxCommand {
    annualSalaryAmount?: number;
}

export class SalaryDto implements ISalaryDto {

    constructor(data?: ISalaryDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
    }

    static fromJS(data: any): SalaryDto {
        data = typeof data === 'object' ? data : {};
        let result = new SalaryDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        return data;
    }
}

export interface ISalaryDto {
}

export class TaxBreakdownDto implements ITaxBreakdownDto {
    grossAnnualSalary?: number;
    grossMonthlySalary?: number;
    netAnnualSalary?: number;
    netMonthlySalary?: number;
    annualTaxPaid?: number;
    monthlyTaxPaid?: number;

    constructor(data?: ITaxBreakdownDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.grossAnnualSalary = _data["grossAnnualSalary"];
            this.grossMonthlySalary = _data["grossMonthlySalary"];
            this.netAnnualSalary = _data["netAnnualSalary"];
            this.netMonthlySalary = _data["netMonthlySalary"];
            this.annualTaxPaid = _data["annualTaxPaid"];
            this.monthlyTaxPaid = _data["monthlyTaxPaid"];
        }
    }

    static fromJS(data: any): TaxBreakdownDto {
        data = typeof data === 'object' ? data : {};
        let result = new TaxBreakdownDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["grossAnnualSalary"] = this.grossAnnualSalary;
        data["grossMonthlySalary"] = this.grossMonthlySalary;
        data["netAnnualSalary"] = this.netAnnualSalary;
        data["netMonthlySalary"] = this.netMonthlySalary;
        data["annualTaxPaid"] = this.annualTaxPaid;
        data["monthlyTaxPaid"] = this.monthlyTaxPaid;
        return data;
    }
}

export interface ITaxBreakdownDto {
    grossAnnualSalary?: number;
    grossMonthlySalary?: number;
    netAnnualSalary?: number;
    netMonthlySalary?: number;
    annualTaxPaid?: number;
    monthlyTaxPaid?: number;
}

export class SwaggerException extends Error {
    override message: string;
    status: number;
    response: string;
    headers: { [key: string]: any; };
    result: any;

    constructor(message: string, status: number, response: string, headers: { [key: string]: any; }, result: any) {
        super();

        this.message = message;
        this.status = status;
        this.response = response;
        this.headers = headers;
        this.result = result;
    }

    protected isSwaggerException = true;

    static isSwaggerException(obj: any): obj is SwaggerException {
        return obj.isSwaggerException === true;
    }
}

function throwException(message: string, status: number, response: string, headers: { [key: string]: any; }, result?: any): Observable<any> {
    if (result !== null && result !== undefined)
        return _observableThrow(result);
    else
        return _observableThrow(new SwaggerException(message, status, response, headers, null));
}

function blobToText(blob: any): Observable<string> {
    return new Observable<string>((observer: any) => {
        if (!blob) {
            observer.next("");
            observer.complete();
        } else {
            let reader = new FileReader();
            reader.onload = event => {
                observer.next((event.target as any).result);
                observer.complete();
            };
            reader.readAsText(blob);
        }
    });
}