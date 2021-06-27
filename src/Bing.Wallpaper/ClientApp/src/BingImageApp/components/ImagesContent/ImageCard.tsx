import React, { useRef, useEffect, useState } from 'react';
import { Image } from '../Image';
import { ImageItemModel } from '../../../api/api';
import { useImagesApi } from '../../hooks/useImagesApi';

import './style.css';

interface ImageCardProps {
    image: ImageItemModel;
}

export const ImageCard = ({ image }: ImageCardProps) => {
    const { showFullSizeImage } = useImagesApi();

    const handleClickImage = () => {
        showFullSizeImage(image);
    };

    return (
        <div
            className="column is-two-thirds-tablet is-half-desktop is-one-third-widescreen is-one-quarter-fullhd clickable "
            onClick={handleClickImage}
        >
            <figure className="is-position-relative">
                <Image
                    imageSrc={`/api/v1.0/files/${encodeURIComponent(
                        `${image.fileName}`,
                    )}`}
                    imageThumbnailSrc={`/api/v1.0/files/${encodeURIComponent(
                        `${image.fileName}`,
                    )}?type=thumbnail`}
                    imgProps={{
                        title: image.title ?? image.fileName ?? '',
                        alt: image.title ?? image.fileName ?? '',
                    }}
                />
                <figcaption className="card-title-over-image rounded-bottom">
                    {image.title ?? image.fileName}
                </figcaption>
            </figure>
        </div>
    );
};
