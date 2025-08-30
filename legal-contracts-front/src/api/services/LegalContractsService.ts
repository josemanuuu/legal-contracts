/* generated using openapi-typescript-codegen -- do not edit */
/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { LegalContract } from '../models/LegalContract';
import type { LegalContractCreateDto } from '../models/LegalContractCreateDto';
import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';
export class LegalContractsService {
    /**
     * @param before
     * @param after
     * @returns LegalContract OK
     * @throws ApiError
     */
    public static getApiLegalContracts(
        before?: string,
        after?: string,
    ): CancelablePromise<Array<LegalContract>> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/api/LegalContracts',
            query: {
                'before': before,
                'after': after,
            },
        });
    }
    /**
     * @param requestBody
     * @returns LegalContract OK
     * @throws ApiError
     */
    public static postApiLegalContracts(
        requestBody?: LegalContractCreateDto,
    ): CancelablePromise<LegalContract> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/api/LegalContracts',
            body: requestBody,
            mediaType: 'application/json',
        });
    }
    /**
     * @param id
     * @returns LegalContract OK
     * @throws ApiError
     */
    public static getApiLegalContracts1(
        id: number,
    ): CancelablePromise<LegalContract> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/api/LegalContracts/{id}',
            path: {
                'id': id,
            },
        });
    }
    /**
     * @param id
     * @param requestBody
     * @returns any OK
     * @throws ApiError
     */
    public static putApiLegalContracts(
        id: number,
        requestBody?: LegalContract,
    ): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'PUT',
            url: '/api/LegalContracts/{id}',
            path: {
                'id': id,
            },
            body: requestBody,
            mediaType: 'application/json',
        });
    }
    /**
     * @param id
     * @returns any OK
     * @throws ApiError
     */
    public static deleteApiLegalContracts(
        id: number,
    ): CancelablePromise<any> {
        return __request(OpenAPI, {
            method: 'DELETE',
            url: '/api/LegalContracts/{id}',
            path: {
                'id': id,
            },
        });
    }
    /**
     * @param maxItems
     * @returns LegalContract OK
     * @throws ApiError
     */
    public static getApiLegalContractsLatest(
        maxItems?: number,
    ): CancelablePromise<Array<LegalContract>> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/api/LegalContracts/latest',
            query: {
                'maxItems': maxItems,
            },
        });
    }
}
