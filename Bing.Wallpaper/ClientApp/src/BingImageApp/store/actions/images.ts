import { ActionType, createAction, createAsyncAction } from 'typesafe-actions';
import {
    LoadImagesRequestModel,
    ImagesApiResponseModel,
    ApiResponseModel,
} from '../../models';

export const loadImages = createAsyncAction(
    'load-images/request',
    'load-images/success',
    'load-images/failure',
)<LoadImagesRequestModel, ImagesApiResponseModel, ApiResponseModel>();

export const resetLoadImagesError = createAction('load-images/reset-error')();

const imagesActions = { loadImages, resetLoadImagesError };

export type ImagesActionTypes =
    | ActionType<typeof loadImages>
    | ActionType<typeof resetLoadImagesError>;

export default imagesActions;
