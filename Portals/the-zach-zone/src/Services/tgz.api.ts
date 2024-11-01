//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v14.1.0.0 (NJsonSchema v11.0.2.0 (Newtonsoft.Json v13.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------

/* tslint:disable */
/* eslint-disable */
// ReSharper disable InconsistentNaming

import axios, { AxiosError, AxiosInstance, AxiosRequestConfig, AxiosResponse, CancelToken } from 'axios';

export class GameClient {
    protected instance: AxiosInstance;
    protected baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(baseUrl?: string, instance?: AxiosInstance) {

        this.instance = instance || axios.create();

        this.baseUrl = baseUrl ?? "";

    }

    getGames(id: number | null | undefined, name: string | null | undefined, category: string | null | undefined, cancelToken?: CancelToken): Promise<GameInfoDto[]> {
        let url_ = this.baseUrl + "/Game/GetGames?";
        if (id !== undefined && id !== null)
            url_ += "Id=" + encodeURIComponent("" + id) + "&";
        if (name !== undefined && name !== null)
            url_ += "Name=" + encodeURIComponent("" + name) + "&";
        if (category !== undefined && category !== null)
            url_ += "Category=" + encodeURIComponent("" + category) + "&";
        url_ = url_.replace(/[?&]$/, "");

        let options_: AxiosRequestConfig = {
            method: "GET",
            url: url_,
            headers: {
                "Accept": "application/json"
            },
            cancelToken
        };

        return this.instance.request(options_).catch((_error: any) => {
            if (isAxiosError(_error) && _error.response) {
                return _error.response;
            } else {
                throw _error;
            }
        }).then((_response: AxiosResponse) => {
            return this.processGetGames(_response);
        });
    }

    protected processGetGames(response: AxiosResponse): Promise<GameInfoDto[]> {
        const status = response.status;
        let _headers: any = {};
        if (response.headers && typeof response.headers === "object") {
            for (const k in response.headers) {
                if (response.headers.hasOwnProperty(k)) {
                    _headers[k] = response.headers[k];
                }
            }
        }
        if (status === 200) {
            const _responseText = response.data;
            let result200: any = null;
            let resultData200  = _responseText;
            if (Array.isArray(resultData200)) {
                result200 = [] as any;
                for (let item of resultData200)
                    result200!.push(GameInfoDto.fromJS(item));
            }
            else {
                result200 = <any>null;
            }
            return Promise.resolve<GameInfoDto[]>(result200);

        } else if (status !== 200 && status !== 204) {
            const _responseText = response.data;
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
        }
        return Promise.resolve<GameInfoDto[]>(null as any);
    }

    loadGame(id: number | undefined, cancelToken?: CancelToken): Promise<string> {
        let url_ = this.baseUrl + "/Game/LoadGame?";
        if (id === null)
            throw new Error("The parameter 'id' cannot be null.");
        else if (id !== undefined)
            url_ += "id=" + encodeURIComponent("" + id) + "&";
        url_ = url_.replace(/[?&]$/, "");

        let options_: AxiosRequestConfig = {
            method: "GET",
            url: url_,
            headers: {
                "Accept": "application/json"
            },
            cancelToken
        };

        return this.instance.request(options_).catch((_error: any) => {
            if (isAxiosError(_error) && _error.response) {
                return _error.response;
            } else {
                throw _error;
            }
        }).then((_response: AxiosResponse) => {
            return this.processLoadGame(_response);
        });
    }

    protected processLoadGame(response: AxiosResponse): Promise<string> {
        const status = response.status;
        let _headers: any = {};
        if (response.headers && typeof response.headers === "object") {
            for (const k in response.headers) {
                if (response.headers.hasOwnProperty(k)) {
                    _headers[k] = response.headers[k];
                }
            }
        }
        if (status === 200) {
            const _responseText = response.data;
            let result200: any = null;
            let resultData200  = _responseText;
                result200 = resultData200 !== undefined ? resultData200 : <any>null;
    
            return Promise.resolve<string>(result200);

        } else if (status !== 200 && status !== 204) {
            const _responseText = response.data;
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
        }
        return Promise.resolve<string>(null as any);
    }

    addGame(name: string | undefined, description: string | undefined, categories: string[] | undefined, file: FileParameter | null | undefined, cancelToken?: CancelToken): Promise<number> {
        let url_ = this.baseUrl + "/Game/AddGame?";
        if (name === null)
            throw new Error("The parameter 'name' cannot be null.");
        else if (name !== undefined)
            url_ += "Name=" + encodeURIComponent("" + name) + "&";
        if (description === null)
            throw new Error("The parameter 'description' cannot be null.");
        else if (description !== undefined)
            url_ += "Description=" + encodeURIComponent("" + description) + "&";
        if (categories === null)
            throw new Error("The parameter 'categories' cannot be null.");
        else if (categories !== undefined)
            categories && categories.forEach(item => { url_ += "Categories=" + encodeURIComponent("" + item) + "&"; });
        url_ = url_.replace(/[?&]$/, "");

        const content_ = new FormData();
        if (file !== null && file !== undefined)
            content_.append("File", file.data, file.fileName ? file.fileName : "File");

        let options_: AxiosRequestConfig = {
            data: content_,
            method: "POST",
            url: url_,
            headers: {
                "Accept": "application/json"
            },
            cancelToken
        };

        return this.instance.request(options_).catch((_error: any) => {
            if (isAxiosError(_error) && _error.response) {
                return _error.response;
            } else {
                throw _error;
            }
        }).then((_response: AxiosResponse) => {
            return this.processAddGame(_response);
        });
    }

    protected processAddGame(response: AxiosResponse): Promise<number> {
        const status = response.status;
        let _headers: any = {};
        if (response.headers && typeof response.headers === "object") {
            for (const k in response.headers) {
                if (response.headers.hasOwnProperty(k)) {
                    _headers[k] = response.headers[k];
                }
            }
        }
        if (status === 200) {
            const _responseText = response.data;
            let result200: any = null;
            let resultData200  = _responseText;
                result200 = resultData200 !== undefined ? resultData200 : <any>null;
    
            return Promise.resolve<number>(result200);

        } else if (status !== 200 && status !== 204) {
            const _responseText = response.data;
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
        }
        return Promise.resolve<number>(null as any);
    }

    deleteGame(id: number | undefined, cancelToken?: CancelToken): Promise<FileResponse> {
        let url_ = this.baseUrl + "/Game/DeleteGame?";
        if (id === null)
            throw new Error("The parameter 'id' cannot be null.");
        else if (id !== undefined)
            url_ += "id=" + encodeURIComponent("" + id) + "&";
        url_ = url_.replace(/[?&]$/, "");

        let options_: AxiosRequestConfig = {
            responseType: "blob",
            method: "DELETE",
            url: url_,
            headers: {
                "Accept": "application/octet-stream"
            },
            cancelToken
        };

        return this.instance.request(options_).catch((_error: any) => {
            if (isAxiosError(_error) && _error.response) {
                return _error.response;
            } else {
                throw _error;
            }
        }).then((_response: AxiosResponse) => {
            return this.processDeleteGame(_response);
        });
    }

    protected processDeleteGame(response: AxiosResponse): Promise<FileResponse> {
        const status = response.status;
        let _headers: any = {};
        if (response.headers && typeof response.headers === "object") {
            for (const k in response.headers) {
                if (response.headers.hasOwnProperty(k)) {
                    _headers[k] = response.headers[k];
                }
            }
        }
        if (status === 200 || status === 206) {
            const contentDisposition = response.headers ? response.headers["content-disposition"] : undefined;
            let fileNameMatch = contentDisposition ? /filename\*=(?:(\\?['"])(.*?)\1|(?:[^\s]+'.*?')?([^;\n]*))/g.exec(contentDisposition) : undefined;
            let fileName = fileNameMatch && fileNameMatch.length > 1 ? fileNameMatch[3] || fileNameMatch[2] : undefined;
            if (fileName) {
                fileName = decodeURIComponent(fileName);
            } else {
                fileNameMatch = contentDisposition ? /filename="?([^"]*?)"?(;|$)/g.exec(contentDisposition) : undefined;
                fileName = fileNameMatch && fileNameMatch.length > 1 ? fileNameMatch[1] : undefined;
            }
            return Promise.resolve({ fileName: fileName, status: status, data: new Blob([response.data], { type: response.headers["content-type"] }), headers: _headers });
        } else if (status !== 200 && status !== 204) {
            const _responseText = response.data;
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
        }
        return Promise.resolve<FileResponse>(null as any);
    }

    getGameComments(gameInfoId: number | undefined, cancelToken?: CancelToken): Promise<CommentDto[]> {
        let url_ = this.baseUrl + "/Game/GetGameComments?";
        if (gameInfoId === null)
            throw new Error("The parameter 'gameInfoId' cannot be null.");
        else if (gameInfoId !== undefined)
            url_ += "gameInfoId=" + encodeURIComponent("" + gameInfoId) + "&";
        url_ = url_.replace(/[?&]$/, "");

        let options_: AxiosRequestConfig = {
            method: "GET",
            url: url_,
            headers: {
                "Accept": "application/json"
            },
            cancelToken
        };

        return this.instance.request(options_).catch((_error: any) => {
            if (isAxiosError(_error) && _error.response) {
                return _error.response;
            } else {
                throw _error;
            }
        }).then((_response: AxiosResponse) => {
            return this.processGetGameComments(_response);
        });
    }

    protected processGetGameComments(response: AxiosResponse): Promise<CommentDto[]> {
        const status = response.status;
        let _headers: any = {};
        if (response.headers && typeof response.headers === "object") {
            for (const k in response.headers) {
                if (response.headers.hasOwnProperty(k)) {
                    _headers[k] = response.headers[k];
                }
            }
        }
        if (status === 200) {
            const _responseText = response.data;
            let result200: any = null;
            let resultData200  = _responseText;
            if (Array.isArray(resultData200)) {
                result200 = [] as any;
                for (let item of resultData200)
                    result200!.push(CommentDto.fromJS(item));
            }
            else {
                result200 = <any>null;
            }
            return Promise.resolve<CommentDto[]>(result200);

        } else if (status !== 200 && status !== 204) {
            const _responseText = response.data;
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
        }
        return Promise.resolve<CommentDto[]>(null as any);
    }

    addGameComment(command: AddGameCommentCommand, cancelToken?: CancelToken): Promise<number> {
        let url_ = this.baseUrl + "/Game/AddGameComment";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(command);

        let options_: AxiosRequestConfig = {
            data: content_,
            method: "POST",
            url: url_,
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            },
            cancelToken
        };

        return this.instance.request(options_).catch((_error: any) => {
            if (isAxiosError(_error) && _error.response) {
                return _error.response;
            } else {
                throw _error;
            }
        }).then((_response: AxiosResponse) => {
            return this.processAddGameComment(_response);
        });
    }

    protected processAddGameComment(response: AxiosResponse): Promise<number> {
        const status = response.status;
        let _headers: any = {};
        if (response.headers && typeof response.headers === "object") {
            for (const k in response.headers) {
                if (response.headers.hasOwnProperty(k)) {
                    _headers[k] = response.headers[k];
                }
            }
        }
        if (status === 200) {
            const _responseText = response.data;
            let result200: any = null;
            let resultData200  = _responseText;
                result200 = resultData200 !== undefined ? resultData200 : <any>null;
    
            return Promise.resolve<number>(result200);

        } else if (status !== 200 && status !== 204) {
            const _responseText = response.data;
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
        }
        return Promise.resolve<number>(null as any);
    }

    deleteGameComment(commentId: number | undefined, cancelToken?: CancelToken): Promise<FileResponse> {
        let url_ = this.baseUrl + "/Game/DeleteGameComment?";
        if (commentId === null)
            throw new Error("The parameter 'commentId' cannot be null.");
        else if (commentId !== undefined)
            url_ += "commentId=" + encodeURIComponent("" + commentId) + "&";
        url_ = url_.replace(/[?&]$/, "");

        let options_: AxiosRequestConfig = {
            responseType: "blob",
            method: "DELETE",
            url: url_,
            headers: {
                "Accept": "application/octet-stream"
            },
            cancelToken
        };

        return this.instance.request(options_).catch((_error: any) => {
            if (isAxiosError(_error) && _error.response) {
                return _error.response;
            } else {
                throw _error;
            }
        }).then((_response: AxiosResponse) => {
            return this.processDeleteGameComment(_response);
        });
    }

    protected processDeleteGameComment(response: AxiosResponse): Promise<FileResponse> {
        const status = response.status;
        let _headers: any = {};
        if (response.headers && typeof response.headers === "object") {
            for (const k in response.headers) {
                if (response.headers.hasOwnProperty(k)) {
                    _headers[k] = response.headers[k];
                }
            }
        }
        if (status === 200 || status === 206) {
            const contentDisposition = response.headers ? response.headers["content-disposition"] : undefined;
            let fileNameMatch = contentDisposition ? /filename\*=(?:(\\?['"])(.*?)\1|(?:[^\s]+'.*?')?([^;\n]*))/g.exec(contentDisposition) : undefined;
            let fileName = fileNameMatch && fileNameMatch.length > 1 ? fileNameMatch[3] || fileNameMatch[2] : undefined;
            if (fileName) {
                fileName = decodeURIComponent(fileName);
            } else {
                fileNameMatch = contentDisposition ? /filename="?([^"]*?)"?(;|$)/g.exec(contentDisposition) : undefined;
                fileName = fileNameMatch && fileNameMatch.length > 1 ? fileNameMatch[1] : undefined;
            }
            return Promise.resolve({ fileName: fileName, status: status, data: new Blob([response.data], { type: response.headers["content-type"] }), headers: _headers });
        } else if (status !== 200 && status !== 204) {
            const _responseText = response.data;
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
        }
        return Promise.resolve<FileResponse>(null as any);
    }

    getGameImages(gameInfoId: number | undefined, cancelToken?: CancelToken): Promise<GameImageDto[]> {
        let url_ = this.baseUrl + "/Game/GetGameImages?";
        if (gameInfoId === null)
            throw new Error("The parameter 'gameInfoId' cannot be null.");
        else if (gameInfoId !== undefined)
            url_ += "gameInfoId=" + encodeURIComponent("" + gameInfoId) + "&";
        url_ = url_.replace(/[?&]$/, "");

        let options_: AxiosRequestConfig = {
            method: "GET",
            url: url_,
            headers: {
                "Accept": "application/json"
            },
            cancelToken
        };

        return this.instance.request(options_).catch((_error: any) => {
            if (isAxiosError(_error) && _error.response) {
                return _error.response;
            } else {
                throw _error;
            }
        }).then((_response: AxiosResponse) => {
            return this.processGetGameImages(_response);
        });
    }

    protected processGetGameImages(response: AxiosResponse): Promise<GameImageDto[]> {
        const status = response.status;
        let _headers: any = {};
        if (response.headers && typeof response.headers === "object") {
            for (const k in response.headers) {
                if (response.headers.hasOwnProperty(k)) {
                    _headers[k] = response.headers[k];
                }
            }
        }
        if (status === 200) {
            const _responseText = response.data;
            let result200: any = null;
            let resultData200  = _responseText;
            if (Array.isArray(resultData200)) {
                result200 = [] as any;
                for (let item of resultData200)
                    result200!.push(GameImageDto.fromJS(item));
            }
            else {
                result200 = <any>null;
            }
            return Promise.resolve<GameImageDto[]>(result200);

        } else if (status !== 200 && status !== 204) {
            const _responseText = response.data;
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
        }
        return Promise.resolve<GameImageDto[]>(null as any);
    }

    addGameImage(gameInfoId: number | undefined, file: FileParameter | null | undefined, cancelToken?: CancelToken): Promise<number> {
        let url_ = this.baseUrl + "/Game/AddGameImage?";
        if (gameInfoId === null)
            throw new Error("The parameter 'gameInfoId' cannot be null.");
        else if (gameInfoId !== undefined)
            url_ += "GameInfoId=" + encodeURIComponent("" + gameInfoId) + "&";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = new FormData();
        if (file !== null && file !== undefined)
            content_.append("File", file.data, file.fileName ? file.fileName : "File");

        let options_: AxiosRequestConfig = {
            data: content_,
            method: "POST",
            url: url_,
            headers: {
                "Accept": "application/json"
            },
            cancelToken
        };

        return this.instance.request(options_).catch((_error: any) => {
            if (isAxiosError(_error) && _error.response) {
                return _error.response;
            } else {
                throw _error;
            }
        }).then((_response: AxiosResponse) => {
            return this.processAddGameImage(_response);
        });
    }

    protected processAddGameImage(response: AxiosResponse): Promise<number> {
        const status = response.status;
        let _headers: any = {};
        if (response.headers && typeof response.headers === "object") {
            for (const k in response.headers) {
                if (response.headers.hasOwnProperty(k)) {
                    _headers[k] = response.headers[k];
                }
            }
        }
        if (status === 200) {
            const _responseText = response.data;
            let result200: any = null;
            let resultData200  = _responseText;
                result200 = resultData200 !== undefined ? resultData200 : <any>null;
    
            return Promise.resolve<number>(result200);

        } else if (status !== 200 && status !== 204) {
            const _responseText = response.data;
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
        }
        return Promise.resolve<number>(null as any);
    }
}

export class HomeClient {
    protected instance: AxiosInstance;
    protected baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(baseUrl?: string, instance?: AxiosInstance) {

        this.instance = instance || axios.create();

        this.baseUrl = baseUrl ?? "";

    }

    home( cancelToken?: CancelToken): Promise<FileResponse> {
        let url_ = this.baseUrl + "/Home";
        url_ = url_.replace(/[?&]$/, "");

        let options_: AxiosRequestConfig = {
            responseType: "blob",
            method: "GET",
            url: url_,
            headers: {
                "Accept": "application/octet-stream"
            },
            cancelToken
        };

        return this.instance.request(options_).catch((_error: any) => {
            if (isAxiosError(_error) && _error.response) {
                return _error.response;
            } else {
                throw _error;
            }
        }).then((_response: AxiosResponse) => {
            return this.processHome(_response);
        });
    }

    protected processHome(response: AxiosResponse): Promise<FileResponse> {
        const status = response.status;
        let _headers: any = {};
        if (response.headers && typeof response.headers === "object") {
            for (const k in response.headers) {
                if (response.headers.hasOwnProperty(k)) {
                    _headers[k] = response.headers[k];
                }
            }
        }
        if (status === 200 || status === 206) {
            const contentDisposition = response.headers ? response.headers["content-disposition"] : undefined;
            let fileNameMatch = contentDisposition ? /filename\*=(?:(\\?['"])(.*?)\1|(?:[^\s]+'.*?')?([^;\n]*))/g.exec(contentDisposition) : undefined;
            let fileName = fileNameMatch && fileNameMatch.length > 1 ? fileNameMatch[3] || fileNameMatch[2] : undefined;
            if (fileName) {
                fileName = decodeURIComponent(fileName);
            } else {
                fileNameMatch = contentDisposition ? /filename="?([^"]*?)"?(;|$)/g.exec(contentDisposition) : undefined;
                fileName = fileNameMatch && fileNameMatch.length > 1 ? fileNameMatch[1] : undefined;
            }
            return Promise.resolve({ fileName: fileName, status: status, data: new Blob([response.data], { type: response.headers["content-type"] }), headers: _headers });
        } else if (status !== 200 && status !== 204) {
            const _responseText = response.data;
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
        }
        return Promise.resolve<FileResponse>(null as any);
    }

    restricted( cancelToken?: CancelToken): Promise<FileResponse> {
        let url_ = this.baseUrl + "/Home/Restricted";
        url_ = url_.replace(/[?&]$/, "");

        let options_: AxiosRequestConfig = {
            responseType: "blob",
            method: "GET",
            url: url_,
            headers: {
                "Accept": "application/octet-stream"
            },
            cancelToken
        };

        return this.instance.request(options_).catch((_error: any) => {
            if (isAxiosError(_error) && _error.response) {
                return _error.response;
            } else {
                throw _error;
            }
        }).then((_response: AxiosResponse) => {
            return this.processRestricted(_response);
        });
    }

    protected processRestricted(response: AxiosResponse): Promise<FileResponse> {
        const status = response.status;
        let _headers: any = {};
        if (response.headers && typeof response.headers === "object") {
            for (const k in response.headers) {
                if (response.headers.hasOwnProperty(k)) {
                    _headers[k] = response.headers[k];
                }
            }
        }
        if (status === 200 || status === 206) {
            const contentDisposition = response.headers ? response.headers["content-disposition"] : undefined;
            let fileNameMatch = contentDisposition ? /filename\*=(?:(\\?['"])(.*?)\1|(?:[^\s]+'.*?')?([^;\n]*))/g.exec(contentDisposition) : undefined;
            let fileName = fileNameMatch && fileNameMatch.length > 1 ? fileNameMatch[3] || fileNameMatch[2] : undefined;
            if (fileName) {
                fileName = decodeURIComponent(fileName);
            } else {
                fileNameMatch = contentDisposition ? /filename="?([^"]*?)"?(;|$)/g.exec(contentDisposition) : undefined;
                fileName = fileNameMatch && fileNameMatch.length > 1 ? fileNameMatch[1] : undefined;
            }
            return Promise.resolve({ fileName: fileName, status: status, data: new Blob([response.data], { type: response.headers["content-type"] }), headers: _headers });
        } else if (status !== 200 && status !== 204) {
            const _responseText = response.data;
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
        }
        return Promise.resolve<FileResponse>(null as any);
    }
}

export class StatisticsClient {
    protected instance: AxiosInstance;
    protected baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(baseUrl?: string, instance?: AxiosInstance) {

        this.instance = instance || axios.create();

        this.baseUrl = baseUrl ?? "";

    }

    addGameplayTimestamp(command: AddGamePlayStatisticCommand, cancelToken?: CancelToken): Promise<FileResponse> {
        let url_ = this.baseUrl + "/Statistics/AddGameplayTimestamp";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(command);

        let options_: AxiosRequestConfig = {
            data: content_,
            responseType: "blob",
            method: "POST",
            url: url_,
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/octet-stream"
            },
            cancelToken
        };

        return this.instance.request(options_).catch((_error: any) => {
            if (isAxiosError(_error) && _error.response) {
                return _error.response;
            } else {
                throw _error;
            }
        }).then((_response: AxiosResponse) => {
            return this.processAddGameplayTimestamp(_response);
        });
    }

    protected processAddGameplayTimestamp(response: AxiosResponse): Promise<FileResponse> {
        const status = response.status;
        let _headers: any = {};
        if (response.headers && typeof response.headers === "object") {
            for (const k in response.headers) {
                if (response.headers.hasOwnProperty(k)) {
                    _headers[k] = response.headers[k];
                }
            }
        }
        if (status === 200 || status === 206) {
            const contentDisposition = response.headers ? response.headers["content-disposition"] : undefined;
            let fileNameMatch = contentDisposition ? /filename\*=(?:(\\?['"])(.*?)\1|(?:[^\s]+'.*?')?([^;\n]*))/g.exec(contentDisposition) : undefined;
            let fileName = fileNameMatch && fileNameMatch.length > 1 ? fileNameMatch[3] || fileNameMatch[2] : undefined;
            if (fileName) {
                fileName = decodeURIComponent(fileName);
            } else {
                fileNameMatch = contentDisposition ? /filename="?([^"]*?)"?(;|$)/g.exec(contentDisposition) : undefined;
                fileName = fileNameMatch && fileNameMatch.length > 1 ? fileNameMatch[1] : undefined;
            }
            return Promise.resolve({ fileName: fileName, status: status, data: new Blob([response.data], { type: response.headers["content-type"] }), headers: _headers });
        } else if (status !== 200 && status !== 204) {
            const _responseText = response.data;
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
        }
        return Promise.resolve<FileResponse>(null as any);
    }
}

export class GameInfoDto implements IGameInfoDto {
    id?: number;
    name?: string;
    description?: string;
    categories?: string[];
    uploadDate?: Date;

    constructor(data?: IGameInfoDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.id = _data["id"];
            this.name = _data["name"];
            this.description = _data["description"];
            if (Array.isArray(_data["categories"])) {
                this.categories = [] as any;
                for (let item of _data["categories"])
                    this.categories!.push(item);
            }
            this.uploadDate = _data["uploadDate"] ? new Date(_data["uploadDate"].toString()) : <any>undefined;
        }
    }

    static fromJS(data: any): GameInfoDto {
        data = typeof data === 'object' ? data : {};
        let result = new GameInfoDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["name"] = this.name;
        data["description"] = this.description;
        if (Array.isArray(this.categories)) {
            data["categories"] = [];
            for (let item of this.categories)
                data["categories"].push(item);
        }
        data["uploadDate"] = this.uploadDate ? formatDate(this.uploadDate) : <any>undefined;
        return data;
    }
}

export interface IGameInfoDto {
    id?: number;
    name?: string;
    description?: string;
    categories?: string[];
    uploadDate?: Date;
}

export class CommentDto implements ICommentDto {
    id?: number;
    authorId?: number;
    authorName?: string;
    content?: string;
    gameInfoId?: number;
    postedOn?: Date;
    updatedOn?: Date | undefined;

    constructor(data?: ICommentDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.id = _data["id"];
            this.authorId = _data["authorId"];
            this.authorName = _data["authorName"];
            this.content = _data["content"];
            this.gameInfoId = _data["gameInfoId"];
            this.postedOn = _data["postedOn"] ? new Date(_data["postedOn"].toString()) : <any>undefined;
            this.updatedOn = _data["updatedOn"] ? new Date(_data["updatedOn"].toString()) : <any>undefined;
        }
    }

    static fromJS(data: any): CommentDto {
        data = typeof data === 'object' ? data : {};
        let result = new CommentDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id;
        data["authorId"] = this.authorId;
        data["authorName"] = this.authorName;
        data["content"] = this.content;
        data["gameInfoId"] = this.gameInfoId;
        data["postedOn"] = this.postedOn ? this.postedOn.toISOString() : <any>undefined;
        data["updatedOn"] = this.updatedOn ? this.updatedOn.toISOString() : <any>undefined;
        return data;
    }
}

export interface ICommentDto {
    id?: number;
    authorId?: number;
    authorName?: string;
    content?: string;
    gameInfoId?: number;
    postedOn?: Date;
    updatedOn?: Date | undefined;
}

export class AddGameCommentCommand implements IAddGameCommentCommand {
    gameInfoId?: number;
    content?: string;

    constructor(data?: IAddGameCommentCommand) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.gameInfoId = _data["gameInfoId"];
            this.content = _data["content"];
        }
    }

    static fromJS(data: any): AddGameCommentCommand {
        data = typeof data === 'object' ? data : {};
        let result = new AddGameCommentCommand();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["gameInfoId"] = this.gameInfoId;
        data["content"] = this.content;
        return data;
    }
}

export interface IAddGameCommentCommand {
    gameInfoId?: number;
    content?: string;
}

export class GameImageDto implements IGameImageDto {
    imageId?: number;
    gameInfoId?: number;
    uploadedOn?: Date;
    data?: string;

    constructor(data?: IGameImageDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.imageId = _data["imageId"];
            this.gameInfoId = _data["gameInfoId"];
            this.uploadedOn = _data["uploadedOn"] ? new Date(_data["uploadedOn"].toString()) : <any>undefined;
            this.data = _data["data"];
        }
    }

    static fromJS(data: any): GameImageDto {
        data = typeof data === 'object' ? data : {};
        let result = new GameImageDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["imageId"] = this.imageId;
        data["gameInfoId"] = this.gameInfoId;
        data["uploadedOn"] = this.uploadedOn ? this.uploadedOn.toISOString() : <any>undefined;
        data["data"] = this.data;
        return data;
    }
}

export interface IGameImageDto {
    imageId?: number;
    gameInfoId?: number;
    uploadedOn?: Date;
    data?: string;
}

export class AddGamePlayStatisticCommand implements IAddGamePlayStatisticCommand {
    gameId?: number;

    constructor(data?: IAddGamePlayStatisticCommand) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.gameId = _data["gameId"];
        }
    }

    static fromJS(data: any): AddGamePlayStatisticCommand {
        data = typeof data === 'object' ? data : {};
        let result = new AddGamePlayStatisticCommand();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["gameId"] = this.gameId;
        return data;
    }
}

export interface IAddGamePlayStatisticCommand {
    gameId?: number;
}

function formatDate(d: Date) {
    return d.getFullYear() + '-' + 
        (d.getMonth() < 9 ? ('0' + (d.getMonth()+1)) : (d.getMonth()+1)) + '-' +
        (d.getDate() < 10 ? ('0' + d.getDate()) : d.getDate());
}

export interface FileParameter {
    data: any;
    fileName: string;
}

export interface FileResponse {
    data: Blob;
    status: number;
    fileName?: string;
    headers?: { [name: string]: any };
}

export class ApiException extends Error {
    message: string;
    status: number;
    response: string;
    headers: { [key: string]: any; };
    result: any;

    constructor(message: string, status: number, response: string, headers: { [key: string]: any; }, result: any) {
        super();

        this.message = message;
        this.status = status;
        this.response = response;
        this.headers = headers;
        this.result = result;
    }

    protected isApiException = true;

    static isApiException(obj: any): obj is ApiException {
        return obj.isApiException === true;
    }
}

function throwException(message: string, status: number, response: string, headers: { [key: string]: any; }, result?: any): any {
    if (result !== null && result !== undefined)
        throw result;
    else
        throw new ApiException(message, status, response, headers, null);
}

function isAxiosError(obj: any): obj is AxiosError {
    return obj && obj.isAxiosError === true;
}