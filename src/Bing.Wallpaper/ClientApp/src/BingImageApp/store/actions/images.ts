import { ActionType, createAction, createAsyncAction } from 'typesafe-actions';
import {
    ObjectApiResponseModel,
    ImageItemModel,
    ImageItemModelIPagedModelApiResponseModel,
} from '../../../api/api';
import { LoadImagesRequestModel } from '../../models';

export const loadImages = createAsyncAction(
    'load-images/request',
    'load-images/success',
    'load-images/failure',
)<
    LoadImagesRequestModel,
    ImageItemModelIPagedModelApiResponseModel,
    ObjectApiResponseModel
>();

export const resetLoadImagesError = createAction('load-images/reset-error')();

export const showFullSizeImage = createAction(
    'load-image/show-full-size',
)<ImageItemModel>();

export const hideFullSizeImage = createAction('load-image/hide-full-size')();

export const imagesActions = {
    loadImages,
    resetLoadImagesError,
    showFullSizeImage,
    hideFullSizeImage,
};

export type ImagesActions = ActionType<typeof imagesActions>;
