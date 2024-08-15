//----------------------
// <auto-generated>
//     Generated using the NSwag toolchain v14.1.0.0 (NJsonSchema v11.0.2.0 (Newtonsoft.Json v13.0.0.0)) (http://NSwag.org)
// </auto-generated>
//----------------------

/* tslint:disable */
/* eslint-disable */
// ReSharper disable InconsistentNaming

import axios, { AxiosError, AxiosInstance, AxiosRequestConfig, AxiosResponse, CancelToken } from 'axios';

export class AccountClient {
    protected instance: AxiosInstance;
    protected baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(baseUrl?: string, instance?: AxiosInstance) {

        this.instance = instance || axios.create();

        this.baseUrl = baseUrl ?? "";

    }

    doesUserExist(email: string | undefined, cancelToken?: CancelToken): Promise<boolean> {
        let url_ = this.baseUrl + "/Account/DoesUserExist?";
        if (email === null)
            throw new Error("The parameter 'email' cannot be null.");
        else if (email !== undefined)
            url_ += "email=" + encodeURIComponent("" + email) + "&";
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
            return this.processDoesUserExist(_response);
        });
    }

    protected processDoesUserExist(response: AxiosResponse): Promise<boolean> {
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
    
            return Promise.resolve<boolean>(result200);

        } else if (status !== 200 && status !== 204) {
            const _responseText = response.data;
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
        }
        return Promise.resolve<boolean>(null as any);
    }

    createAccount(command: CreateAccountCommand, cancelToken?: CancelToken): Promise<ZachZoneCommand> {
        let url_ = this.baseUrl + "/Account/CreateAccount";
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
            return this.processCreateAccount(_response);
        });
    }

    protected processCreateAccount(response: AxiosResponse): Promise<ZachZoneCommand> {
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
            result200 = ZachZoneCommand.fromJS(resultData200);
            return Promise.resolve<ZachZoneCommand>(result200);

        } else if (status !== 200 && status !== 204) {
            const _responseText = response.data;
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
        }
        return Promise.resolve<ZachZoneCommand>(null as any);
    }

    login(email: string | undefined, password: string | undefined, cancelToken?: CancelToken): Promise<FileResponse> {
        let url_ = this.baseUrl + "/Account/Login?";
        if (email === null)
            throw new Error("The parameter 'email' cannot be null.");
        else if (email !== undefined)
            url_ += "email=" + encodeURIComponent("" + email) + "&";
        if (password === null)
            throw new Error("The parameter 'password' cannot be null.");
        else if (password !== undefined)
            url_ += "password=" + encodeURIComponent("" + password) + "&";
        url_ = url_.replace(/[?&]$/, "");

        let options_: AxiosRequestConfig = {
            responseType: "blob",
            method: "POST",
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
            return this.processLogin(_response);
        });
    }

    protected processLogin(response: AxiosResponse): Promise<FileResponse> {
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

    logout( cancelToken?: CancelToken): Promise<string> {
        let url_ = this.baseUrl + "/Account/Logout";
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
            return this.processLogout(_response);
        });
    }

    protected processLogout(response: AxiosResponse): Promise<string> {
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

    userInfo( cancelToken?: CancelToken): Promise<UserInfoDto> {
        let url_ = this.baseUrl + "/Account/UserInfo";
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
            return this.processUserInfo(_response);
        });
    }

    protected processUserInfo(response: AxiosResponse): Promise<UserInfoDto> {
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
            result200 = UserInfoDto.fromJS(resultData200);
            return Promise.resolve<UserInfoDto>(result200);

        } else if (status !== 200 && status !== 204) {
            const _responseText = response.data;
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
        }
        return Promise.resolve<UserInfoDto>(null as any);
    }

    getGeneralInformation( cancelToken?: CancelToken): Promise<GeneralInformationDto> {
        let url_ = this.baseUrl + "/Account/GetGeneralInformation";
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
            return this.processGetGeneralInformation(_response);
        });
    }

    protected processGetGeneralInformation(response: AxiosResponse): Promise<GeneralInformationDto> {
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
            result200 = GeneralInformationDto.fromJS(resultData200);
            return Promise.resolve<GeneralInformationDto>(result200);

        } else if (status !== 200 && status !== 204) {
            const _responseText = response.data;
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
        }
        return Promise.resolve<GeneralInformationDto>(null as any);
    }

    updateGeneralInformation(command: UpdateGeneralInformationCommand, cancelToken?: CancelToken): Promise<FileResponse> {
        let url_ = this.baseUrl + "/Account/UpdateGeneralInformation";
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
            return this.processUpdateGeneralInformation(_response);
        });
    }

    protected processUpdateGeneralInformation(response: AxiosResponse): Promise<FileResponse> {
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

    resetPassword(command: ResetPasswordCommand, cancelToken?: CancelToken): Promise<FileResponse> {
        let url_ = this.baseUrl + "/Account/ResetPassword";
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
            return this.processResetPassword(_response);
        });
    }

    protected processResetPassword(response: AxiosResponse): Promise<FileResponse> {
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

export class ZachZoneCommand implements IZachZoneCommand {
    infos?: { [key: string]: string; };
    warnings?: { [key: string]: string; };
    errors?: { [key: string]: string; };
    isValid?: boolean;

    constructor(data?: IZachZoneCommand) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            if (_data["infos"]) {
                this.infos = {} as any;
                for (let key in _data["infos"]) {
                    if (_data["infos"].hasOwnProperty(key))
                        (<any>this.infos)![key] = _data["infos"][key];
                }
            }
            if (_data["warnings"]) {
                this.warnings = {} as any;
                for (let key in _data["warnings"]) {
                    if (_data["warnings"].hasOwnProperty(key))
                        (<any>this.warnings)![key] = _data["warnings"][key];
                }
            }
            if (_data["errors"]) {
                this.errors = {} as any;
                for (let key in _data["errors"]) {
                    if (_data["errors"].hasOwnProperty(key))
                        (<any>this.errors)![key] = _data["errors"][key];
                }
            }
            this.isValid = _data["isValid"];
        }
    }

    static fromJS(data: any): ZachZoneCommand {
        data = typeof data === 'object' ? data : {};
        let result = new ZachZoneCommand();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        if (this.infos) {
            data["infos"] = {};
            for (let key in this.infos) {
                if (this.infos.hasOwnProperty(key))
                    (<any>data["infos"])[key] = (<any>this.infos)[key];
            }
        }
        if (this.warnings) {
            data["warnings"] = {};
            for (let key in this.warnings) {
                if (this.warnings.hasOwnProperty(key))
                    (<any>data["warnings"])[key] = (<any>this.warnings)[key];
            }
        }
        if (this.errors) {
            data["errors"] = {};
            for (let key in this.errors) {
                if (this.errors.hasOwnProperty(key))
                    (<any>data["errors"])[key] = (<any>this.errors)[key];
            }
        }
        data["isValid"] = this.isValid;
        return data;
    }
}

export interface IZachZoneCommand {
    infos?: { [key: string]: string; };
    warnings?: { [key: string]: string; };
    errors?: { [key: string]: string; };
    isValid?: boolean;
}

export class CreateAccountCommand implements ICreateAccountCommand {
    email?: string;
    password?: string;
    confirmPassword?: string;

    constructor(data?: ICreateAccountCommand) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.email = _data["email"];
            this.password = _data["password"];
            this.confirmPassword = _data["confirmPassword"];
        }
    }

    static fromJS(data: any): CreateAccountCommand {
        data = typeof data === 'object' ? data : {};
        let result = new CreateAccountCommand();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["email"] = this.email;
        data["password"] = this.password;
        data["confirmPassword"] = this.confirmPassword;
        return data;
    }
}

export interface ICreateAccountCommand {
    email?: string;
    password?: string;
    confirmPassword?: string;
}

export class UserInfoDto implements IUserInfoDto {
    email?: string;

    constructor(data?: IUserInfoDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.email = _data["email"];
        }
    }

    static fromJS(data: any): UserInfoDto {
        data = typeof data === 'object' ? data : {};
        let result = new UserInfoDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["email"] = this.email;
        return data;
    }
}

export interface IUserInfoDto {
    email?: string;
}

export class GeneralInformationDto implements IGeneralInformationDto {
    email?: string | undefined;
    firstName?: string | undefined;
    lastName?: string | undefined;

    constructor(data?: IGeneralInformationDto) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.email = _data["email"];
            this.firstName = _data["firstName"];
            this.lastName = _data["lastName"];
        }
    }

    static fromJS(data: any): GeneralInformationDto {
        data = typeof data === 'object' ? data : {};
        let result = new GeneralInformationDto();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["email"] = this.email;
        data["firstName"] = this.firstName;
        data["lastName"] = this.lastName;
        return data;
    }
}

export interface IGeneralInformationDto {
    email?: string | undefined;
    firstName?: string | undefined;
    lastName?: string | undefined;
}

export class UpdateGeneralInformationCommand implements IUpdateGeneralInformationCommand {
    firstName?: string;
    lastName?: string;

    constructor(data?: IUpdateGeneralInformationCommand) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.firstName = _data["firstName"];
            this.lastName = _data["lastName"];
        }
    }

    static fromJS(data: any): UpdateGeneralInformationCommand {
        data = typeof data === 'object' ? data : {};
        let result = new UpdateGeneralInformationCommand();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["firstName"] = this.firstName;
        data["lastName"] = this.lastName;
        return data;
    }
}

export interface IUpdateGeneralInformationCommand {
    firstName?: string;
    lastName?: string;
}

export class ResetPasswordCommand implements IResetPasswordCommand {
    resetToken?: string;
    newPassword?: string;
    confirmPassword?: string;

    constructor(data?: IResetPasswordCommand) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.resetToken = _data["resetToken"];
            this.newPassword = _data["newPassword"];
            this.confirmPassword = _data["confirmPassword"];
        }
    }

    static fromJS(data: any): ResetPasswordCommand {
        data = typeof data === 'object' ? data : {};
        let result = new ResetPasswordCommand();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["resetToken"] = this.resetToken;
        data["newPassword"] = this.newPassword;
        data["confirmPassword"] = this.confirmPassword;
        return data;
    }
}

export interface IResetPasswordCommand {
    resetToken?: string;
    newPassword?: string;
    confirmPassword?: string;
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