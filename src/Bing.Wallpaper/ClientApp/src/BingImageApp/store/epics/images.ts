import { combineEpics, Epic } from 'redux-observable';
import { isActionOf } from 'typesafe-actions';
import { from, of } from 'rxjs';
import { filter, map, switchMap, catchError } from 'rxjs/operators';
import { imagesActions, ImagesActions } from '../actions/images';
import { RootState } from '../reducers';
import { ApiClient, ErrorResponse } from '../../services';

export const loadImagesEpic: Epic<
    ImagesActions,
    ImagesActions,
    RootState,
    ApiClient
> = (action$, state$, api) =>
    action$.pipe(
        filter(isActionOf(imagesActions.loadImages.request)),
        switchMap((action) => {
            const { page, take } = action.payload;
            return from(
                api.images.apiv10ImagesGetAll({ page, take }, undefined),
            ).pipe(
                map((response) =>
                    imagesActions.loadImages.success(response.data),
                ),
                catchError((errorResponse: ErrorResponse) =>
                    of(imagesActions.loadImages.failure(errorResponse.data)),
                ),
            );
        }),
    );

const imagesEpic = combineEpics(loadImagesEpic);

export default imagesEpic;
export type ImagesEpic = ReturnType<typeof imagesEpic>;
