import { combineReducers } from 'redux';
import { createReducer } from 'typesafe-actions';
import { ApiResponseModel, LogModel } from '../../../api/api';
import { logsActions, LogsActions } from '../actions/logs';

export const isLoadingLogs = createReducer<boolean, LogsActions>(false)
    .handleAction([logsActions.loadLogs.request], (state, action) => true)
    .handleAction(
        [logsActions.loadLogs.success, logsActions.loadLogs.failure],
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

            logsActions.resetLoadLogsError,
        ],
        (state, action) => null,
    )
    .handleAction(
        [logsActions.loadLogs.failure],
        (state, action) => action.payload,
    );

export const logs = createReducer<LogModel[], LogsActions>([]).handleAction(
    [logsActions.loadLogs.success],
    (state, action) => {
        if (action.payload.data?.currentPage === 1) {
            return [...(action.payload.data?.items ?? [])];
        } else {
            return [...state.concat(action.payload.data?.items ?? [])];
        }
    },
);

export const hasMoreLogs = createReducer<boolean, LogsActions>(true)
    .handleAction([logsActions.loadLogs.success], (state, action) => {
        const itemsCount = (action.payload.data?.items ?? []).length;
        const limit = action.payload.data?.limit ?? 0;
        console.info(`hasMoreLogs, ${action.type}`, limit, itemsCount);
        return limit === itemsCount;
    })
    .handleAction(
        [logsActions.loadLogs.request, logsActions.loadLogs.failure],
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
