import axios from "axios";
import { Client } from "./apiModels";

const axiosInstance = axios.create({
    baseURL: process.env.VUE_APP_WEB_API_SERVER,
    timeout: 50000,
});

const $api = new Client(undefined, axiosInstance);
export default $api;
