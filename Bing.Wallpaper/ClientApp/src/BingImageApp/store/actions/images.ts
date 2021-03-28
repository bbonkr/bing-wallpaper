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

export const resetLoadImagesError = createAction('load-images/reset-error')();

export const showFullSizeImage = createAction(
    'load-image/show-full-size',
)<ImageItemModel>();

export const hideFullSizeImage = createAction('load-image/hide-full-size')();

const imagesActions = {
    loadImages,
    resetLoadImagesError,
    showFullSizeImage,
    hideFullSizeImage,
};

export type ImagesActionTypes =
    | ActionType<typeof loadImages>
    | ActionType<typeof resetLoadImagesError>
    | ActionType<typeof showFullSizeImage>
    | ActionType<typeof hideFullSizeImage>;

export default imagesActions;
