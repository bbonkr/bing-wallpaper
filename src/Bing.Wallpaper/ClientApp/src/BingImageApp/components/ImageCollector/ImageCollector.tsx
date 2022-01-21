import React, { useState } from 'react';
import { Content, Section } from '../Layouts';
import Loading from '../Loading';
import { CollectorForm } from './CollectorForm';
import { useSWRConfig } from 'swr';
import { ApiClient } from '../../services';
import { ApiResponseModel } from '../../models';
import { UiHelper } from '../../lib/UiHelper';

export const ImageCollector = () => {
    const [isLoadingCollectImages, setIsLoadingCollectImages] = useState(false);
    const [collectResult, setCollectResult] = useState<ApiResponseModel>();
    const [hasError, setHasError] = useState(false);

    const { mutate } = useSWRConfig();

    const handleCollect = (startIndex: number, take: number) => {
        setIsLoadingCollectImages((_) => true);
        setHasError((_) => false);
        mutate(
            ['POST:/api/bingImages'],
            (_: any) => {
                return new ApiClient().bingImage
                    .apiv10BingImagesCollectImages({
                        bingImageServiceGetRequestModel: {
                            startIndex: startIndex,
                            take: take,
                        },
                    })
                    .then((res) => res.data);
            },
            false,
        )
            .then((res) => {
                console.info('mutate result', res);
                const data: ApiResponseModel = res;
                setCollectResult((_) => data);
            })
            .catch((res) => {
                const data: ApiResponseModel = res;
                setCollectResult((_) => data);
                setHasError((_) => true);
            })
            .finally(() => {
                setIsLoadingCollectImages(false);
            });
    };

    return (
        <Content classNames={[]}>
            <Section
                title="Collector"
                useHero
                heroSize="is-small"
                heroColor="is-warning"
            />
            <Section>
                <CollectorForm
                    isLoading={isLoadingCollectImages}
                    onCollect={handleCollect}
                />
            </Section>
            <Section>
                {isLoadingCollectImages ? (
                    <Loading message="Collecting ..." />
                ) : (
                    <div
                        className={UiHelper.getClassNames(
                            ...[
                                'is-flex-grow-1',
                                'is-flex',
                                'is-flex-direction-column',
                                'is-justify-content-center',
                                'is-align-items-center',
                                hasError ? 'has-text-danger' : 'has-text-info',
                            ].filter(Boolean),
                        )}
                    >
                        {collectResult?.message}
                    </div>
                )}
            </Section>
        </Content>
    );
};

export default ImageCollector;
