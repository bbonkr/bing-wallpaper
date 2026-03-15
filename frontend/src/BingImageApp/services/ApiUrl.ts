const DEFAULT_PUBLIC_API_BASE_URL = '/api/v1.0';

const trimTrailingSlash = (value: string): string => value.replace(/\/+$/, '');

export const getPublicApiBaseUrl = (): string => {
    const configured = process.env.NEXT_PUBLIC_API_BASE_URL?.trim();

    if (!configured) {
        return DEFAULT_PUBLIC_API_BASE_URL;
    }

    return trimTrailingSlash(configured);
};

export const getApiOrigin = (): string | undefined => {
    const rawUrl =
        (typeof window === 'undefined'
            ? process.env.INTERNAL_API_BASE_URL
            : process.env.NEXT_PUBLIC_API_BASE_URL) ?? '';
    const trimmed = rawUrl.trim();

    if (!trimmed) {
        return undefined;
    }

    try {
        return new URL(trimmed).origin;
    } catch {
        return undefined;
    }
};

export const buildFileUrl = (
    fileName: string,
    type?: 'thumbnail',
): string => {
    const encodedName = encodeURIComponent(fileName);
    const baseUrl = getPublicApiBaseUrl();
    const query = type ? `?type=${encodeURIComponent(type)}` : '';

    return `${baseUrl}/files/${encodedName}${query}`;
};
