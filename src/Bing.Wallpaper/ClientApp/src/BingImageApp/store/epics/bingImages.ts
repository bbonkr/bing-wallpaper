import { combineEpics, Epic } from 'redux-observable';
import { isActionOf } from 'typesafe-actions';
import { from, of } from 'rxjs';
import { filter, map, switchMap, catchError } from 'rxjs/operators';
import { bingImagesActions, BingImagesActions } from '../actions/bingImages';
import { RootState } from '../reducers';
import { ApiClient, ErrorResponse } from '../../services';

export const collectImagesEpic: Epic<
    BingImagesActions,
    BingImagesActions,
    RootState,
    ApiClient
> = (action$, state$, api) =>
    action$.pipe(
        filter(isActionOf(bingImagesActions.collectImages.request)),
        switchMap((action) => {
            return from(
                api.bingImage.apiv10BingImagesCollectImages(action.payload),
            ).pipe(
                map((response) =>
                    bingImagesActions.collectImages.success(response.data),
                ),
                catchError((errorResponse: ErrorResponse) =>
                    of(
                        bingImagesActions.collectImages.failure(
                            errorResponse.data,
                        ),
                    ),
                ),
            );
        }),
    );

const bingImagesEpic = combineEpics(collectImagesEpic);

export default bingImagesEpic;
export type BingImagesEpic = ReturnType<typeof bingImagesEpic>;
