import React from 'react';
import { Image } from '../Image';
import { ImageItemModel } from '../../../api/api';
import { useImagesApi } from '../../hooks/useImagesApi';
import { buildFileUrl } from '../../services';


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
                    imageSrc={buildFileUrl(`${image.fileName}`)}
                    imageThumbnailSrc={buildFileUrl(
                        `${image.fileName}`,
                        'thumbnail',
                    )}
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
