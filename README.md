<h4 align="center"><b>** THIS IS STILL WORK IN PROGRESS **</b></h4>

<div id="top"></div>

<!-- PROJECT LOGO -->
<br />
<div align="center">
  <h1 align="center">Firefly III Importer</h1>

  <p align="center">
    This is an importer for the populair free and open source personal finance manager.
    <br />
    <a href="https://github.com/bramhoven/Firefly-III-Importer/wiki"><strong>Explore the docs »</strong></a>
    <br />
    <br />
    <a href="https://github.com/bramhoven/Firefly-III-Importer/">Report Bug</a>
    ·
    <a href="https://github.com/bramhoven/Firefly-III-Importer/">Request Feature</a>
  </p>
</div>



<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#roadmap">Roadmap</a></li>
    <li><a href="#contributing">Contributing</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
## About The Project

The goal is to create an easy to use importer that anyone can use to import their financial information into Firefly III.
The idea came after I tried too manually import CSV's or any of the other data importers. I could not get it to work reliably and if it worked the importer did not parse the transaction names correctly. This Firefly III Importer tries to solve it by providing a transaction converter per bank (W.I.P.), to always get the correct names and description. 

<p align="right">(<a href="#top">back to top</a>)</p>



### Built With

* [ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-6.0)
* [Docker](https://www.docker.com/)
* [Nordigen](https://nordigen.com/en/)

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- GETTING STARTED -->
## Getting Started

We have created a Docker image for ease of use. This way you can run it without installing any other dependencies.

### Prerequisites

1. Make sure Docker is installed
2. Have a running Firefly III instance
3. Get the access token for the Firefly III instance
4. Create a free account on Nordigen
5. Make sure Docker is installed

<!-- USAGE EXAMPLES -->
## Usage

We can run the Firefly III Importer by running the following command:
#### Windows
```console
foo@bar:~$ docker run `
  --rm `
  -e FIREFLY_III_ACCESS_TOKEN={Firefly III access token} `
  -e FIREFLY_III_URL={Firefly III instance url} `
  -e NORDIGEN_SECRET_KEY={Nordigen secret key} `
  -e NORDIGEN_SECRET_ID={Nordigen base url} `
  -p 8080:80 `
  bramhoven/firefly-importer:latest
```

Other:
```console
foo@bar:~$ docker run \
  --rm \
  -e FIREFLY_III_ACCESS_TOKEN={Firefly III access token} \
  -e FIREFLY_III_URL={Firefly III instance url} \
  -e NORDIGEN_SECRET_KEY={Nordigen secret key} \
  -e NORDIGEN_SECRET_ID={Nordigen base url} \
  -p 8080:80 \
  bramhoven/firefly-importer:latest
```

This will launch the web UI which is accessible at [http://localhost:8080](http://localhost:8080).

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- ROADMAP -->
## Roadmap

- [[IN PROGRESS]](https://github.com/bramhoven/Firefly-III-Importer/issues/2) Importing status updates via websockets to web UI
- [[TODO]](https://github.com/bramhoven/Firefly-III-Importer/issues/3) Configuring importer via web UI
- [[TODO]](https://github.com/bramhoven/Firefly-III-Importer/issues/5) Automatically import new transactions

See the [open issues](https://github.com/bramhoven/Firefly-III-Importer/issues) for a full list of proposed features (and known issues).

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- CONTRIBUTING -->
## Contributing

Contributions are what make the open source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

If you have a suggestion that would make this better, please fork the repo and create a pull request. You can also simply open an issue with the tag "enhancement".
Don't forget to give the project a star! Thanks again!

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- LICENSE -->
## License

Distributed under GNU General Public License v3.0. See `LICENSE` for more information.

<p align="right">(<a href="#top">back to top</a>)</p>



<!-- CONTACT -->
## Contact

Bram Hoven [info@bramhoven.nl](mailto:info@bramhoven.nl)

Project Link: [https://github.com/bramhoven/Firefly-III-Importer](https://github.com/bramhoven/Firefly-III-Importer)

<p align="right">(<a href="#top">back to top</a>)</p>
