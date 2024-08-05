<table>
<tr>
<th style="padding-right: 3rem;">Story Points</th>
<th style="padding-right: 3rem;">Related Sites</th>
<th style="padding-right: 3rem;">Priority</th>
</tr>
<tr>
<td>
<select>
<option>1</option>
<option>2</option>
<option>3</option>
<option>4</option>
<option>8</option>
<option>13</option>
<option>21</option>
</select>
</td>
<td>
<select>
<option>None</option>
<option>All</option>
<option>Agile Life</option>
<option>Crapazon</option>
<option>Planet Earth</option>
<option>Pocket Persona</option>
<option>The Crypto Zone</option>
<option>The Game Zone</option>
<option>The School Zone</option>
<option>The Zach Zone</option>
</select>
</td>
<td>
<select>
<option>Low</option>
<option>Normal</option>
<option>High</option>
<option>Highest</option>
</select></td>
</tr>
</table>

> [!summary] Story
> Create the build and release pipelines in Azure DevOps.

> [!done] Acceptance Criteria
> - There are two sets of pipelines. One for production and one for development
> - The development pipelines run when the development branch is pushed to
> - The production pipelines run when the production branch is pushed to.
> - An Azure agent is running on a server in my laundry room. This is the server that will be hosting the site. 
> - The local server will be running docker images and use nginx to reverse proxy the sites on the containers. 
> - HTTPS works
> - Hyperlinks between my sites still work.
> - Nginx hosts each site using the dryrlent.ddns.net/{siteName}

> [!info] Implementation Details
> Provide information on how to accomplish this task...

> [!seealso] See Also
> [Installing nginx on Windows](https://nginx.org/en/docs/windows.html)
