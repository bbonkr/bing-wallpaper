import { ApiResponseModel } from './ApiResponseModel';
import { PagedModel } from './PagedModel';

export type ImagesApiResponseModel = ApiResponseModel<PagedModel<ImageItemModel>>;

export interface ImageItemModel {
    id: string;
    fileName: string;
    fileExtension: string;
    title?: string;
    copyright?: string;
    copyrightLink?: string;
    fileSize: number;
    createdAt: Date;
}

export interface ImageDetailModel extends ImageItemModel {
    filePath: string;
    contentType: string;
}
