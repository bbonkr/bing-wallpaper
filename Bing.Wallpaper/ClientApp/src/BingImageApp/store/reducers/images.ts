import { combineReducers } from 'redux';
import { createReducer } from 'typesafe-actions';
import { ApiResponseModel, ImageItemModel } from '../../models';
import imagesActions, { ImagesActionTypes } from '../actions/images';

export const isLoadingImages = createReducer<boolean, ImagesActionTypes>(false)
    .handleAction([imagesActions.loadImages.request], (state, action) => true)
    .handleAction(
        [imagesActions.loadImages.success, imagesActions.loadImages.failure],
        (state, action) => false,
    );

export const loadImagesError = createReducer<
    ApiResponseModel | undefined,
    ImagesActionTypes
>(undefined)
    .handleAction(
        [
            imagesActions.loadImages.request,
            imagesActions.loadImages.success,
            imagesActions.resetLoadImagesError,
        ],
        (state, action) => undefined,
    )
    .handleAction(
        [imagesActions.loadImages.failure],
        (state, action) => action.payload,
    );

export const images = createReducer<
    ImageItemModel[] | undefined,
    ImagesActionTypes
>([]).handleAction([imagesActions.loadImages.success], (state, action) => {
    return [...(state ?? []), ...(action.payload.data ?? [])];
});

export const hadMoreImages = createReducer<boolean, ImagesActionTypes>(true)
    .handleAction(
        [imagesActions.loadImages.success],
        (state, action) => (action.payload.data ?? []).length > 0,
    )
    .handleAction(
        [imagesActions.loadImages.request, imagesActions.loadImages.failure],
        (state, action) => true,
    );

const imagesState = combineReducers({
    isLoadingImages,
    images,
    hadMoreImages,
});

export default imagesState;
export type ImagesState = ReturnType<typeof imagesState>;
