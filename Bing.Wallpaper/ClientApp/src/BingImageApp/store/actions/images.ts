import { ActionType, createAction, createAsyncAction } from 'typesafe-actions';
import {
    LoadImagesRequestModel,
    ImagesApiResponseModel,
    ApiResponseModel,
    ImageItemModel,
} from '../../models';

export const loadImages = createAsyncAction(
    'load-images/request',
    'load-images/success',
    'load-images/failure',
)<LoadImagesRequestModel, ImagesApiResponseModel, ApiResponseModel>();

export const appendImages = createAsyncAction(
    'append-images/request',
    'append-images/success',
    'append-images/failure',
)<LoadImagesRequestModel, ImagesApiResponseModel, ApiResponseModel>();

export const resetLoadImagesError = createAction('load-images/reset-error')();

export const showFullSizeImage = createAction(
    'load-image/show-full-size',
)<ImageItemModel>();

export const hideFullSizeImage = createAction('load-image/hide-full-size')();

export const imagesActions = {
    loadImages,
    appendImages,
    resetLoadImagesError,
    showFullSizeImage,
    hideFullSizeImage,
};

export type ImagesActions = ActionType<typeof imagesActions>;
