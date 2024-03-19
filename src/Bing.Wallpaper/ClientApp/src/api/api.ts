/* tslint:disable */
/* eslint-disable */
/**
 * Today Bing images
 * Today Bing images api
 *
 * The version of the OpenAPI document: 1.0
 * 
 *
 * NOTE: This class is auto generated by OpenAPI Generator (https://openapi-generator.tech).
 * https://openapi-generator.tech
 * Do not edit the class manually.
 */


import { Configuration } from './configuration';
import globalAxios, { AxiosPromise, AxiosInstance } from 'axios';
// Some imports not used depending on template conditions
// @ts-ignore
import { DUMMY_BASE_URL, assertParamExists, setApiKeyToObject, setBasicAuthToObject, setBearerAuthToObject, setOAuthToObject, setSearchParams, serializeDataIfNeeded, toPathString, createRequestFunction } from './common';
// @ts-ignore
import { BASE_PATH, COLLECTION_FORMATS, RequestArgs, BaseAPI, RequiredError } from './base';

/**
 * 
 * @export
 * @interface BingImageServiceGetRequestModel
 */
export interface BingImageServiceGetRequestModel {
    /**
     * 
     * @type {string}
     * @memberof BingImageServiceGetRequestModel
     */
    format?: string | null;
    /**
     * 
     * @type {number}
     * @memberof BingImageServiceGetRequestModel
     */
    startIndex?: number;
    /**
     * 
     * @type {number}
     * @memberof BingImageServiceGetRequestModel
     */
    take?: number;
    /**
     * 
     * @type {string}
     * @memberof BingImageServiceGetRequestModel
     */
    market?: string | null;
}
/**
 * 
 * @export
 * @interface ImageItemModel
 */
export interface ImageItemModel {
    /**
     * 
     * @type {string}
     * @memberof ImageItemModel
     */
    id?: string | null;
    /**
     * 
     * @type {string}
     * @memberof ImageItemModel
     */
    title?: string | null;
    /**
     * 
     * @type {string}
     * @memberof ImageItemModel
     */
    fileName?: string | null;
    /**
     * 
     * @type {string}
     * @memberof ImageItemModel
     */
    fileExtension?: string | null;
    /**
     * 
     * @type {number}
     * @memberof ImageItemModel
     */
    fileSize?: number;
    /**
     * 
     * @type {number}
     * @memberof ImageItemModel
     */
    createdAt?: number;
    /**
     * 
     * @type {string}
     * @memberof ImageItemModel
     */
    copyright?: string | null;
    /**
     * 
     * @type {string}
     * @memberof ImageItemModel
     */
    copyrightLink?: string | null;
    /**
     * 
     * @type {number}
     * @memberof ImageItemModel
     */
    width?: number;
    /**
     * 
     * @type {number}
     * @memberof ImageItemModel
     */
    height?: number;
}
/**
 * 
 * @export
 * @interface ImageItemModelPagedModel
 */
export interface ImageItemModelPagedModel {
    /**
     * 
     * @type {number}
     * @memberof ImageItemModelPagedModel
     */
    currentPage?: number;
    /**
     * 
     * @type {number}
     * @memberof ImageItemModelPagedModel
     */
    limit?: number;
    /**
     * 
     * @type {number}
     * @memberof ImageItemModelPagedModel
     */
    totalItems?: number;
    /**
     * 
     * @type {number}
     * @memberof ImageItemModelPagedModel
     */
    totalPages?: number;
    /**
     * 
     * @type {Array<ImageItemModel>}
     * @memberof ImageItemModelPagedModel
     */
    items?: Array<ImageItemModel> | null;
    /**
     * 
     * @type {boolean}
     * @memberof ImageItemModelPagedModel
     */
    hasNextPage?: boolean;
    /**
     * 
     * @type {boolean}
     * @memberof ImageItemModelPagedModel
     */
    hasPreviousPage?: boolean;
    /**
     * 
     * @type {boolean}
     * @memberof ImageItemModelPagedModel
     */
    isFirstPage?: boolean;
    /**
     * 
     * @type {boolean}
     * @memberof ImageItemModelPagedModel
     */
    isLastPage?: boolean;
}
/**
 * 
 * @export
 * @interface LogModel
 */
export interface LogModel {
    /**
     * 
     * @type {number}
     * @memberof LogModel
     */
    id?: number;
    /**
     * 
     * @type {string}
     * @memberof LogModel
     */
    message?: string | null;
    /**
     * 
     * @type {string}
     * @memberof LogModel
     */
    messageTemplate?: string | null;
    /**
     * 
     * @type {string}
     * @memberof LogModel
     */
    level?: string | null;
    /**
     * 
     * @type {number}
     * @memberof LogModel
     */
    timeStamp?: number;
    /**
     * 
     * @type {string}
     * @memberof LogModel
     */
    exception?: string | null;
    /**
     * 
     * @type {string}
     * @memberof LogModel
     */
    logEvent?: string | null;
    /**
     * 
     * @type {string}
     * @memberof LogModel
     */
    payload?: string | null;
    /**
     * 
     * @type {string}
     * @memberof LogModel
     */
    queryString?: string | null;
    /**
     * 
     * @type {string}
     * @memberof LogModel
     */
    userRoles?: string | null;
    /**
     * 
     * @type {string}
     * @memberof LogModel
     */
    userIp?: string | null;
    /**
     * 
     * @type {string}
     * @memberof LogModel
     */
    requestUri?: string | null;
    /**
     * 
     * @type {string}
     * @memberof LogModel
     */
    httpMethod?: string | null;
    /**
     * 
     * @type {boolean}
     * @memberof LogModel
     */
    isResolved?: boolean | null;
    /**
     * 
     * @type {string}
     * @memberof LogModel
     */
    userAgent?: string | null;
    /**
     * 
     * @type {string}
     * @memberof LogModel
     */
    errors?: string | null;
    /**
     * 
     * @type {number}
     * @memberof LogModel
     */
    resolvedAt?: number | null;
}
/**
 * 
 * @export
 * @interface LogModelPagedModel
 */
export interface LogModelPagedModel {
    /**
     * 
     * @type {number}
     * @memberof LogModelPagedModel
     */
    currentPage?: number;
    /**
     * 
     * @type {number}
     * @memberof LogModelPagedModel
     */
    limit?: number;
    /**
     * 
     * @type {number}
     * @memberof LogModelPagedModel
     */
    totalItems?: number;
    /**
     * 
     * @type {number}
     * @memberof LogModelPagedModel
     */
    totalPages?: number;
    /**
     * 
     * @type {Array<LogModel>}
     * @memberof LogModelPagedModel
     */
    items?: Array<LogModel> | null;
    /**
     * 
     * @type {boolean}
     * @memberof LogModelPagedModel
     */
    hasNextPage?: boolean;
    /**
     * 
     * @type {boolean}
     * @memberof LogModelPagedModel
     */
    hasPreviousPage?: boolean;
    /**
     * 
     * @type {boolean}
     * @memberof LogModelPagedModel
     */
    isFirstPage?: boolean;
    /**
     * 
     * @type {boolean}
     * @memberof LogModelPagedModel
     */
    isLastPage?: boolean;
}
/**
 * 
 * @export
 * @interface LogModelPagedModelApiResponseModel
 */
export interface LogModelPagedModelApiResponseModel {
    /**
     * 
     * @type {number}
     * @memberof LogModelPagedModelApiResponseModel
     */
    statusCode?: number;
    /**
     * 
     * @type {string}
     * @memberof LogModelPagedModelApiResponseModel
     */
    message?: string | null;
    /**
     * 
     * @type {string}
     * @memberof LogModelPagedModelApiResponseModel
     */
    instance?: string | null;
    /**
     * 
     * @type {string}
     * @memberof LogModelPagedModelApiResponseModel
     */
    path?: string | null;
    /**
     * 
     * @type {string}
     * @memberof LogModelPagedModelApiResponseModel
     */
    method?: string | null;
    /**
     * 
     * @type {LogModelPagedModel}
     * @memberof LogModelPagedModelApiResponseModel
     */
    data?: LogModelPagedModel;
}
/**
 * 
 * @export
 * @interface ObjectApiResponseModel
 */
export interface ObjectApiResponseModel {
    /**
     * 
     * @type {number}
     * @memberof ObjectApiResponseModel
     */
    statusCode?: number;
    /**
     * 
     * @type {string}
     * @memberof ObjectApiResponseModel
     */
    message?: string | null;
    /**
     * 
     * @type {string}
     * @memberof ObjectApiResponseModel
     */
    instance?: string | null;
    /**
     * 
     * @type {string}
     * @memberof ObjectApiResponseModel
     */
    path?: string | null;
    /**
     * 
     * @type {string}
     * @memberof ObjectApiResponseModel
     */
    method?: string | null;
    /**
     * 
     * @type {any}
     * @memberof ObjectApiResponseModel
     */
    data?: any | null;
}
/**
 * 
 * @export
 * @interface ProblemDetails
 */
export interface ProblemDetails {
    [key: string]: any | any;

    /**
     * 
     * @type {string}
     * @memberof ProblemDetails
     */
    type?: string | null;
    /**
     * 
     * @type {string}
     * @memberof ProblemDetails
     */
    title?: string | null;
    /**
     * 
     * @type {number}
     * @memberof ProblemDetails
     */
    status?: number | null;
    /**
     * 
     * @type {string}
     * @memberof ProblemDetails
     */
    detail?: string | null;
    /**
     * 
     * @type {string}
     * @memberof ProblemDetails
     */
    instance?: string | null;
}

/**
 * BingImagesApi - axios parameter creator
 * @export
 */
export const BingImagesApiAxiosParamCreator = function (configuration?: Configuration) {
    return {
        /**
         * 
         * @param {BingImageServiceGetRequestModel} [bingImageServiceGetRequestModel] 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        apiv10BingImagesCollectImages: async (bingImageServiceGetRequestModel?: BingImageServiceGetRequestModel, options: any = {}): Promise<RequestArgs> => {
            const localVarPath = `/api/v1/BingImages`;
            // use dummy base URL string because the URL constructor only accepts absolute URLs.
            const localVarUrlObj = new URL(localVarPath, DUMMY_BASE_URL);
            let baseOptions;
            if (configuration) {
                baseOptions = configuration.baseOptions;
            }

            const localVarRequestOptions = { method: 'POST', ...baseOptions, ...options};
            const localVarHeaderParameter = {} as any;
            const localVarQueryParameter = {} as any;


    
            localVarHeaderParameter['Content-Type'] = 'application/json';

            setSearchParams(localVarUrlObj, localVarQueryParameter, options.query);
            let headersFromBaseOptions = baseOptions && baseOptions.headers ? baseOptions.headers : {};
            localVarRequestOptions.headers = {...localVarHeaderParameter, ...headersFromBaseOptions, ...options.headers};
            localVarRequestOptions.data = serializeDataIfNeeded(bingImageServiceGetRequestModel, localVarRequestOptions, configuration)

            return {
                url: toPathString(localVarUrlObj),
                options: localVarRequestOptions,
            };
        },
    }
};

/**
 * BingImagesApi - functional programming interface
 * @export
 */
export const BingImagesApiFp = function(configuration?: Configuration) {
    const localVarAxiosParamCreator = BingImagesApiAxiosParamCreator(configuration)
    return {
        /**
         * 
         * @param {BingImageServiceGetRequestModel} [bingImageServiceGetRequestModel] 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        async apiv10BingImagesCollectImages(bingImageServiceGetRequestModel?: BingImageServiceGetRequestModel, options?: any): Promise<(axios?: AxiosInstance, basePath?: string) => AxiosPromise<ObjectApiResponseModel>> {
            const localVarAxiosArgs = await localVarAxiosParamCreator.apiv10BingImagesCollectImages(bingImageServiceGetRequestModel, options);
            return createRequestFunction(localVarAxiosArgs, globalAxios, BASE_PATH, configuration);
        },
    }
};

/**
 * BingImagesApi - factory interface
 * @export
 */
export const BingImagesApiFactory = function (configuration?: Configuration, basePath?: string, axios?: AxiosInstance) {
    const localVarFp = BingImagesApiFp(configuration)
    return {
        /**
         * 
         * @param {BingImageServiceGetRequestModel} [bingImageServiceGetRequestModel] 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        apiv10BingImagesCollectImages(bingImageServiceGetRequestModel?: BingImageServiceGetRequestModel, options?: any): AxiosPromise<ObjectApiResponseModel> {
            return localVarFp.apiv10BingImagesCollectImages(bingImageServiceGetRequestModel, options).then((request) => request(axios, basePath));
        },
    };
};

/**
 * Request parameters for apiv10BingImagesCollectImages operation in BingImagesApi.
 * @export
 * @interface BingImagesApiApiv10BingImagesCollectImagesRequest
 */
export interface BingImagesApiApiv10BingImagesCollectImagesRequest {
    /**
     * 
     * @type {BingImageServiceGetRequestModel}
     * @memberof BingImagesApiApiv10BingImagesCollectImages
     */
    readonly bingImageServiceGetRequestModel?: BingImageServiceGetRequestModel
}

/**
 * BingImagesApi - object-oriented interface
 * @export
 * @class BingImagesApi
 * @extends {BaseAPI}
 */
export class BingImagesApi extends BaseAPI {
    /**
     * 
     * @param {BingImagesApiApiv10BingImagesCollectImagesRequest} requestParameters Request parameters.
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof BingImagesApi
     */
    public apiv10BingImagesCollectImages(requestParameters: BingImagesApiApiv10BingImagesCollectImagesRequest = {}, options?: any) {
        return BingImagesApiFp(this.configuration).apiv10BingImagesCollectImages(requestParameters.bingImageServiceGetRequestModel, options).then((request) => request(this.axios, this.basePath));
    }
}


/**
 * FilesApi - axios parameter creator
 * @export
 */
export const FilesApiAxiosParamCreator = function (configuration?: Configuration) {
    return {
        /**
         * 
         * @param {string} fileName 
         * @param {string} [type] 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        apiv10FilesGetFileByFileName: async (fileName: string, type?: string, options: any = {}): Promise<RequestArgs> => {
            // verify required parameter 'fileName' is not null or undefined
            assertParamExists('apiv10FilesGetFileByFileName', 'fileName', fileName)
            const localVarPath = `/api/v1/Files/{fileName}`
                .replace(`{${"fileName"}}`, encodeURIComponent(String(fileName)));
            // use dummy base URL string because the URL constructor only accepts absolute URLs.
            const localVarUrlObj = new URL(localVarPath, DUMMY_BASE_URL);
            let baseOptions;
            if (configuration) {
                baseOptions = configuration.baseOptions;
            }

            const localVarRequestOptions = { method: 'GET', ...baseOptions, ...options};
            const localVarHeaderParameter = {} as any;
            const localVarQueryParameter = {} as any;

            if (type !== undefined) {
                localVarQueryParameter['type'] = type;
            }


    
            setSearchParams(localVarUrlObj, localVarQueryParameter, options.query);
            let headersFromBaseOptions = baseOptions && baseOptions.headers ? baseOptions.headers : {};
            localVarRequestOptions.headers = {...localVarHeaderParameter, ...headersFromBaseOptions, ...options.headers};

            return {
                url: toPathString(localVarUrlObj),
                options: localVarRequestOptions,
            };
        },
        /**
         * 
         * @param {string} id 
         * @param {string} [type] 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        apiv10FilesGetFileById: async (id: string, type?: string, options: any = {}): Promise<RequestArgs> => {
            // verify required parameter 'id' is not null or undefined
            assertParamExists('apiv10FilesGetFileById', 'id', id)
            const localVarPath = `/api/v1/Files/{id}`
                .replace(`{${"id"}}`, encodeURIComponent(String(id)));
            // use dummy base URL string because the URL constructor only accepts absolute URLs.
            const localVarUrlObj = new URL(localVarPath, DUMMY_BASE_URL);
            let baseOptions;
            if (configuration) {
                baseOptions = configuration.baseOptions;
            }

            const localVarRequestOptions = { method: 'GET', ...baseOptions, ...options};
            const localVarHeaderParameter = {} as any;
            const localVarQueryParameter = {} as any;

            if (type !== undefined) {
                localVarQueryParameter['type'] = type;
            }


    
            setSearchParams(localVarUrlObj, localVarQueryParameter, options.query);
            let headersFromBaseOptions = baseOptions && baseOptions.headers ? baseOptions.headers : {};
            localVarRequestOptions.headers = {...localVarHeaderParameter, ...headersFromBaseOptions, ...options.headers};

            return {
                url: toPathString(localVarUrlObj),
                options: localVarRequestOptions,
            };
        },
    }
};

/**
 * FilesApi - functional programming interface
 * @export
 */
export const FilesApiFp = function(configuration?: Configuration) {
    const localVarAxiosParamCreator = FilesApiAxiosParamCreator(configuration)
    return {
        /**
         * 
         * @param {string} fileName 
         * @param {string} [type] 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        async apiv10FilesGetFileByFileName(fileName: string, type?: string, options?: any): Promise<(axios?: AxiosInstance, basePath?: string) => AxiosPromise<any>> {
            const localVarAxiosArgs = await localVarAxiosParamCreator.apiv10FilesGetFileByFileName(fileName, type, options);
            return createRequestFunction(localVarAxiosArgs, globalAxios, BASE_PATH, configuration);
        },
        /**
         * 
         * @param {string} id 
         * @param {string} [type] 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        async apiv10FilesGetFileById(id: string, type?: string, options?: any): Promise<(axios?: AxiosInstance, basePath?: string) => AxiosPromise<any>> {
            const localVarAxiosArgs = await localVarAxiosParamCreator.apiv10FilesGetFileById(id, type, options);
            return createRequestFunction(localVarAxiosArgs, globalAxios, BASE_PATH, configuration);
        },
    }
};

/**
 * FilesApi - factory interface
 * @export
 */
export const FilesApiFactory = function (configuration?: Configuration, basePath?: string, axios?: AxiosInstance) {
    const localVarFp = FilesApiFp(configuration)
    return {
        /**
         * 
         * @param {string} fileName 
         * @param {string} [type] 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        apiv10FilesGetFileByFileName(fileName: string, type?: string, options?: any): AxiosPromise<any> {
            return localVarFp.apiv10FilesGetFileByFileName(fileName, type, options).then((request) => request(axios, basePath));
        },
        /**
         * 
         * @param {string} id 
         * @param {string} [type] 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        apiv10FilesGetFileById(id: string, type?: string, options?: any): AxiosPromise<any> {
            return localVarFp.apiv10FilesGetFileById(id, type, options).then((request) => request(axios, basePath));
        },
    };
};

/**
 * Request parameters for apiv10FilesGetFileByFileName operation in FilesApi.
 * @export
 * @interface FilesApiApiv10FilesGetFileByFileNameRequest
 */
export interface FilesApiApiv10FilesGetFileByFileNameRequest {
    /**
     * 
     * @type {string}
     * @memberof FilesApiApiv10FilesGetFileByFileName
     */
    readonly fileName: string

    /**
     * 
     * @type {string}
     * @memberof FilesApiApiv10FilesGetFileByFileName
     */
    readonly type?: string
}

/**
 * Request parameters for apiv10FilesGetFileById operation in FilesApi.
 * @export
 * @interface FilesApiApiv10FilesGetFileByIdRequest
 */
export interface FilesApiApiv10FilesGetFileByIdRequest {
    /**
     * 
     * @type {string}
     * @memberof FilesApiApiv10FilesGetFileById
     */
    readonly id: string

    /**
     * 
     * @type {string}
     * @memberof FilesApiApiv10FilesGetFileById
     */
    readonly type?: string
}

/**
 * FilesApi - object-oriented interface
 * @export
 * @class FilesApi
 * @extends {BaseAPI}
 */
export class FilesApi extends BaseAPI {
    /**
     * 
     * @param {FilesApiApiv10FilesGetFileByFileNameRequest} requestParameters Request parameters.
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof FilesApi
     */
    public apiv10FilesGetFileByFileName(requestParameters: FilesApiApiv10FilesGetFileByFileNameRequest, options?: any) {
        return FilesApiFp(this.configuration).apiv10FilesGetFileByFileName(requestParameters.fileName, requestParameters.type, options).then((request) => request(this.axios, this.basePath));
    }

    /**
     * 
     * @param {FilesApiApiv10FilesGetFileByIdRequest} requestParameters Request parameters.
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof FilesApi
     */
    public apiv10FilesGetFileById(requestParameters: FilesApiApiv10FilesGetFileByIdRequest, options?: any) {
        return FilesApiFp(this.configuration).apiv10FilesGetFileById(requestParameters.id, requestParameters.type, options).then((request) => request(this.axios, this.basePath));
    }
}


/**
 * ImagesApi - axios parameter creator
 * @export
 */
export const ImagesApiAxiosParamCreator = function (configuration?: Configuration) {
    return {
        /**
         * 
         * @param {number} [page] 
         * @param {number} [take] 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        apiv10ImagesGetAll: async (page?: number, take?: number, options: any = {}): Promise<RequestArgs> => {
            const localVarPath = `/api/v1/Images`;
            // use dummy base URL string because the URL constructor only accepts absolute URLs.
            const localVarUrlObj = new URL(localVarPath, DUMMY_BASE_URL);
            let baseOptions;
            if (configuration) {
                baseOptions = configuration.baseOptions;
            }

            const localVarRequestOptions = { method: 'GET', ...baseOptions, ...options};
            const localVarHeaderParameter = {} as any;
            const localVarQueryParameter = {} as any;

            if (page !== undefined) {
                localVarQueryParameter['page'] = page;
            }

            if (take !== undefined) {
                localVarQueryParameter['take'] = take;
            }


    
            setSearchParams(localVarUrlObj, localVarQueryParameter, options.query);
            let headersFromBaseOptions = baseOptions && baseOptions.headers ? baseOptions.headers : {};
            localVarRequestOptions.headers = {...localVarHeaderParameter, ...headersFromBaseOptions, ...options.headers};

            return {
                url: toPathString(localVarUrlObj),
                options: localVarRequestOptions,
            };
        },
    }
};

/**
 * ImagesApi - functional programming interface
 * @export
 */
export const ImagesApiFp = function(configuration?: Configuration) {
    const localVarAxiosParamCreator = ImagesApiAxiosParamCreator(configuration)
    return {
        /**
         * 
         * @param {number} [page] 
         * @param {number} [take] 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        async apiv10ImagesGetAll(page?: number, take?: number, options?: any): Promise<(axios?: AxiosInstance, basePath?: string) => AxiosPromise<ImageItemModelPagedModel>> {
            const localVarAxiosArgs = await localVarAxiosParamCreator.apiv10ImagesGetAll(page, take, options);
            return createRequestFunction(localVarAxiosArgs, globalAxios, BASE_PATH, configuration);
        },
    }
};

/**
 * ImagesApi - factory interface
 * @export
 */
export const ImagesApiFactory = function (configuration?: Configuration, basePath?: string, axios?: AxiosInstance) {
    const localVarFp = ImagesApiFp(configuration)
    return {
        /**
         * 
         * @param {number} [page] 
         * @param {number} [take] 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        apiv10ImagesGetAll(page?: number, take?: number, options?: any): AxiosPromise<ImageItemModelPagedModel> {
            return localVarFp.apiv10ImagesGetAll(page, take, options).then((request) => request(axios, basePath));
        },
    };
};

/**
 * Request parameters for apiv10ImagesGetAll operation in ImagesApi.
 * @export
 * @interface ImagesApiApiv10ImagesGetAllRequest
 */
export interface ImagesApiApiv10ImagesGetAllRequest {
    /**
     * 
     * @type {number}
     * @memberof ImagesApiApiv10ImagesGetAll
     */
    readonly page?: number

    /**
     * 
     * @type {number}
     * @memberof ImagesApiApiv10ImagesGetAll
     */
    readonly take?: number
}

/**
 * ImagesApi - object-oriented interface
 * @export
 * @class ImagesApi
 * @extends {BaseAPI}
 */
export class ImagesApi extends BaseAPI {
    /**
     * 
     * @param {ImagesApiApiv10ImagesGetAllRequest} requestParameters Request parameters.
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof ImagesApi
     */
    public apiv10ImagesGetAll(requestParameters: ImagesApiApiv10ImagesGetAllRequest = {}, options?: any) {
        return ImagesApiFp(this.configuration).apiv10ImagesGetAll(requestParameters.page, requestParameters.take, options).then((request) => request(this.axios, this.basePath));
    }
}


/**
 * LogsApi - axios parameter creator
 * @export
 */
export const LogsApiAxiosParamCreator = function (configuration?: Configuration) {
    return {
        /**
         * 
         * @param {number} [page] 
         * @param {number} [take] 
         * @param {string} [level] 
         * @param {string} [keyword] 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        apiv10LogsGetAll: async (page?: number, take?: number, level?: string, keyword?: string, options: any = {}): Promise<RequestArgs> => {
            const localVarPath = `/api/v1/Logs`;
            // use dummy base URL string because the URL constructor only accepts absolute URLs.
            const localVarUrlObj = new URL(localVarPath, DUMMY_BASE_URL);
            let baseOptions;
            if (configuration) {
                baseOptions = configuration.baseOptions;
            }

            const localVarRequestOptions = { method: 'GET', ...baseOptions, ...options};
            const localVarHeaderParameter = {} as any;
            const localVarQueryParameter = {} as any;

            if (page !== undefined) {
                localVarQueryParameter['page'] = page;
            }

            if (take !== undefined) {
                localVarQueryParameter['take'] = take;
            }

            if (level !== undefined) {
                localVarQueryParameter['level'] = level;
            }

            if (keyword !== undefined) {
                localVarQueryParameter['keyword'] = keyword;
            }


    
            setSearchParams(localVarUrlObj, localVarQueryParameter, options.query);
            let headersFromBaseOptions = baseOptions && baseOptions.headers ? baseOptions.headers : {};
            localVarRequestOptions.headers = {...localVarHeaderParameter, ...headersFromBaseOptions, ...options.headers};

            return {
                url: toPathString(localVarUrlObj),
                options: localVarRequestOptions,
            };
        },
    }
};

/**
 * LogsApi - functional programming interface
 * @export
 */
export const LogsApiFp = function(configuration?: Configuration) {
    const localVarAxiosParamCreator = LogsApiAxiosParamCreator(configuration)
    return {
        /**
         * 
         * @param {number} [page] 
         * @param {number} [take] 
         * @param {string} [level] 
         * @param {string} [keyword] 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        async apiv10LogsGetAll(page?: number, take?: number, level?: string, keyword?: string, options?: any): Promise<(axios?: AxiosInstance, basePath?: string) => AxiosPromise<LogModelPagedModelApiResponseModel>> {
            const localVarAxiosArgs = await localVarAxiosParamCreator.apiv10LogsGetAll(page, take, level, keyword, options);
            return createRequestFunction(localVarAxiosArgs, globalAxios, BASE_PATH, configuration);
        },
    }
};

/**
 * LogsApi - factory interface
 * @export
 */
export const LogsApiFactory = function (configuration?: Configuration, basePath?: string, axios?: AxiosInstance) {
    const localVarFp = LogsApiFp(configuration)
    return {
        /**
         * 
         * @param {number} [page] 
         * @param {number} [take] 
         * @param {string} [level] 
         * @param {string} [keyword] 
         * @param {*} [options] Override http request option.
         * @throws {RequiredError}
         */
        apiv10LogsGetAll(page?: number, take?: number, level?: string, keyword?: string, options?: any): AxiosPromise<LogModelPagedModelApiResponseModel> {
            return localVarFp.apiv10LogsGetAll(page, take, level, keyword, options).then((request) => request(axios, basePath));
        },
    };
};

/**
 * Request parameters for apiv10LogsGetAll operation in LogsApi.
 * @export
 * @interface LogsApiApiv10LogsGetAllRequest
 */
export interface LogsApiApiv10LogsGetAllRequest {
    /**
     * 
     * @type {number}
     * @memberof LogsApiApiv10LogsGetAll
     */
    readonly page?: number

    /**
     * 
     * @type {number}
     * @memberof LogsApiApiv10LogsGetAll
     */
    readonly take?: number

    /**
     * 
     * @type {string}
     * @memberof LogsApiApiv10LogsGetAll
     */
    readonly level?: string

    /**
     * 
     * @type {string}
     * @memberof LogsApiApiv10LogsGetAll
     */
    readonly keyword?: string
}

/**
 * LogsApi - object-oriented interface
 * @export
 * @class LogsApi
 * @extends {BaseAPI}
 */
export class LogsApi extends BaseAPI {
    /**
     * 
     * @param {LogsApiApiv10LogsGetAllRequest} requestParameters Request parameters.
     * @param {*} [options] Override http request option.
     * @throws {RequiredError}
     * @memberof LogsApi
     */
    public apiv10LogsGetAll(requestParameters: LogsApiApiv10LogsGetAllRequest = {}, options?: any) {
        return LogsApiFp(this.configuration).apiv10LogsGetAll(requestParameters.page, requestParameters.take, requestParameters.level, requestParameters.keyword, options).then((request) => request(this.axios, this.basePath));
    }
}


