import { combineReducers } from 'redux';
import { createReducer } from 'typesafe-actions';
import { ApiResponseModel, LogModel } from '../../models';
import { logsActions, LogsActions } from '../actions/logs';

export const isLoadingLogs = createReducer<boolean, LogsActions>(false)
    .handleAction(
        [logsActions.loadLogs.request, logsActions.appendLogs.request],
        (state, action) => true,
    )
    .handleAction(
        [
            logsActions.loadLogs.success,
            logsActions.loadLogs.failure,
            logsActions.appendLogs.success,
            logsActions.appendLogs.failure,
        ],
        (state, action) => false,
    );

export const loadLogsError = createReducer<
    ApiResponseModel | null,
    LogsActions
>(null)
    .handleAction(
        [
            logsActions.loadLogs.request,
            logsActions.loadLogs.success,
            logsActions.appendLogs.request,
            logsActions.appendLogs.success,
            logsActions.resetLoadLogsError,
        ],
        (state, action) => null,
    )
    .handleAction(
        [logsActions.loadLogs.failure, logsActions.appendLogs.failure],
        (state, action) => action.payload,
    );

export const logs = createReducer<LogModel[] | undefined, LogsActions>([])
    .handleAction([logsActions.loadLogs.success], (state, action) => {
        return [...(action.payload.data?.items ?? [])];
    })
    .handleAction([logsActions.appendLogs.success], (state, action) => {
        return [...(state ?? []).concat(action.payload.data?.items ?? [])];
    });

export const hasMoreLogs = createReducer<boolean, LogsActions>(true)
    .handleAction(
        [logsActions.loadLogs.success, logsActions.appendLogs.success],
        (state, action) => {
            const itemsCount = (action.payload.data?.items ?? []).length;
            const limit = action.payload.data?.limit ?? 0;
            return limit === itemsCount;
        },
    )
    .handleAction(
        [
            logsActions.loadLogs.request,
            logsActions.loadLogs.failure,
            logsActions.appendLogs.request,
            logsActions.appendLogs.failure,
        ],
        (state, action) => true,
    );

const logsState = combineReducers({
    isLoadingLogs,
    logs,
    hasMoreLogs,
    loadLogsError,
});

export default logsState;
export type LogsState = ReturnType<typeof logsState>;
